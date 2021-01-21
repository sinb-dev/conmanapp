using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Conman.Models {
    public class Token {
        [Key]
        public string TokenId {get;set;} = "";
        public string Owner {get;set;} = "";
        public string Class {get;set;} = "";
        public DateTime LastUse {get;set;} = DateTime.MinValue;
        public int Type {get;set;} = 1;
        public bool IsAdmin() => Type == 2;
        public void Save() {
            
        }

        public static string CreateToken(int length = 32)  
        {  
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";  
            Random random = new Random();  
        
            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];  
            for (int i = 0; i < length; i++)  
            {  
                chars[i] = validChars[random.Next(0, validChars.Length)];  
            }  
            return new string(chars);  
        }  
        public static Token CreateAdmin(string owner,string course="admin") {
            return new Token {
                TokenId = CreateToken(32),
                Owner = owner,
                Class = course,
                Type = 2,
            };
        }
        public static Token CreateUser(string owner,string course) {
            return new Token {
                TokenId = CreateToken(32),
                Owner = owner,
                Class = course,
                Type = 1,
            };
        }
        public static Token CreateRequest(string owner,string course) {
            return new Token {
                TokenId = CreateToken(32),
                Owner = owner,
                Class = course,
                Type = 0,
            };
        }
    }
    
}