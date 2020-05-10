﻿using System.Collections.Generic;
using BookClub.Entities;

namespace BookClub.Data
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        void SubmitNewBook(Book bookToSubmit, int submitter);
    }
}
