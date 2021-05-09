using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using testRazorPage.Model;

namespace testRazorPage.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Book = new Book();
            if(id == null)
            {
                //create
                return Page();
            }

            //update
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            else return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    //create
                    _db.Book.Add(Book);
                    TempData["UpsertMessage"] = "Create successful";
               }
                else
                {
                    //update all record inside dtb
                    _db.Book.Update(Book);
                    TempData["UpsertMessage"] = "Update successful";

                    //update specified record

                    //var BookFromDb = await _db.Book.FindAsync(Book.Id);
                    //BookFromDb.Name = Book.Name;
                    //BookFromDb.Author = Book.Author;
                    //BookFromDb.ISBN = Book.ISBN;
                }
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
