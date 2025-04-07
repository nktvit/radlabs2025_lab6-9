using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ProductModel.GRN
{
    public class GRNRepository : IGRN<GRN>, IDisposable
    {
        private readonly ProductDBContext context;

        public GRNRepository(ProductDBContext context)
        {
            this.context = context;
        }

        public void Add(GRN entity)
        {
            context.GRNs.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<GRN> entities)
        {
            context.GRNs.AddRange(entities);
            context.SaveChanges();
        }

        public IEnumerable<GRN> Find(Expression<Func<GRN, bool>> predicate)
        {
            return context.GRNs.Where(predicate);
        }

        public GRN Get(int id)
        {
            // Return a GRN with all its GRN Lines and the associated Products details
            return context.GRNs
                .Include(g => g.GRNLines)
                .ThenInclude(gl => gl.associatedProduct)
                .FirstOrDefault(g => g.GrnID == id);
        }

        public IEnumerable<GRN> GetAll()
        {
            // Return all GRNs with their GRN Lines and the associated Products details
            return context.GRNs
                .Include(g => g.GRNLines)
                .ThenInclude(gl => gl.associatedProduct)
                .ToList();
        }

        public void Remove(GRN entity)
        {
            context.GRNs.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<GRN> entities)
        {
            context.GRNs.RemoveRange(entities);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
