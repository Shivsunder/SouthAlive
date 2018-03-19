using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SouthAlive.Data;
using SouthAlive.Models.SampleEditor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace SouthAlive.Controllers
{
    public class EditorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;

        public EditorController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Editor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editor.ToListAsync());
        }


        public IActionResult PostWrite()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostWrite(string title, string editor)
        {
            ViewBag.EditorText = $"{title}<hr />{editor}";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(
            ICollection<IFormFile> upload,
            string CKEditorFuncNum,
            string CKEditor,
            string langCode
        )
        {
            string imgPath = "";
            string msg = "";
            string uploadFolder = Path.Combine(_env.WebRootPath, "files");

            try
            {
                foreach (var file in upload)
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(
                            DateTime.Now.ToString("yyyyMMdd-HHMMssff") + "-"
                            + ContentDispositionHeaderValue.Parse(
                                file.ContentDisposition).FileName.Trim());

                        using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        imgPath = Url.Content("/files/" + fileName);
                        msg = "이미지가 정상적으로 업로드 되었습니다.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "오류가 발생했습니다. 오류메시지" + ex.Message;
            }
            string r = $"<script>window.parent.CKEDITOR.tools.callFunction({CKEditorFuncNum},\"{imgPath}\",\"{msg}\");</script>";

            return Content(r, "text/html");
        }


        // GET: Editor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editor
                .SingleOrDefaultAsync(m => m.EditorId == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // GET: Editor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EditorId,Title,Content")] Editor editor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(editor);
        }

        // GET: Editor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editor.SingleOrDefaultAsync(m => m.EditorId == id);
            if (editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        // POST: Editor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EditorId,Title,Content")] Editor editor)
        {
            if (id != editor.EditorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorExists(editor.EditorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(editor);
        }

        // GET: Editor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editor
                .SingleOrDefaultAsync(m => m.EditorId == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // POST: Editor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editor = await _context.Editor.SingleOrDefaultAsync(m => m.EditorId == id);
            _context.Editor.Remove(editor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EditorExists(int id)
        {
            return _context.Editor.Any(e => e.EditorId == id);
        }
    }
}


