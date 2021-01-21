using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Conman.Models;
using System.Linq;

namespace Conman 
{
    public class ConmanContext : DbContext 
    {
        public DbSet<Container> Containers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Token> Tokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //The full monty: Data Source=ServerName;Initial Catalog=DatabaseName;Integrated Security=False;User Id=userid;Password=password;MultipleActiveResultSets=True
            options.UseSqlServer(@"Data Source=localhost;Initial Catalog=conman;User id=SA;Password=T0engage;MultipleActiveResultSets=True");
        }

        public bool isAdmin(string token) {
            var result = Tokens
                .Where(b => b.IsAdmin() ).ToList();

            return result.Count > 0;
        }
        
        public bool ValidateUserToken(string token) 
        {
            var result = Tokens
                .Where(b => b.TokenId == token ).ToList();

            if (result.Count == 0) return false;

            result[0].LastUse = DateTime.Now;
            SaveChanges();
            return true;
        }
        public bool ValidateAdminToken(string token) 
        {
            var result = Tokens
                .Where(b => b.TokenId == token && b.Type == 2 ).ToList();

            if (result.Count == 0) return false;

            result[0].LastUse = DateTime.Now;
            SaveChanges();
            return true;
        }

        public Token GetToken(string tokenId) 
        {
            /*using (var context = new ConmanContext()) {
                //return context.Tokens.Where( x => x.TokenId == tokenId).Select( x => x)
                var result = context.Tokens.Where( x => x.TokenId == tokenId ).ToList();
                return result.Count > 0 ? result[0] : null;
            }*/
            var result = Tokens.Where( x => x.TokenId == tokenId ).ToList();
            return result.Count > 0 ? result[0] : null;
        }
        public Reservation GetReservation(short port)
        {
            using (var context = new ConmanContext()) {
                var result = context.Reservations.Where( x => x.Port == port).ToList();
                return result.Count > 0 ? result[0] : null;
            }
        }
    }
}