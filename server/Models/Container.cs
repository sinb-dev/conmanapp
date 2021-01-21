using System.ComponentModel.DataAnnotations;

namespace Conman.Models {
    public class Container {
        
        [Key]
        public short Port {get;set;}
        public Token Token {get;set;}
        public string Title {get;set;}
        public string Name {get;set;}
        public string Image {get;set;}
        public string Command {get;set;}

    }
}