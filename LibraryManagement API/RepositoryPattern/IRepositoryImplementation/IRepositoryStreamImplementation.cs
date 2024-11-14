using LibraryManagement_API.Models;
using LibraryManagement_API.RepositoryPattern.IRepository;
using System;

namespace LibraryManagement_API.RepositoryPattern.IRepositoryImplementation
{
    public class IRepositoryStreamImplementation : IRepositoryStream
    {
        private readonly MyDbContext _appDbContext;

        public IRepositoryStreamImplementation(MyDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
    }
}
