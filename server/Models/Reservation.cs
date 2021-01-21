using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conman.Models {
    public class Reservation {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Port {get;set;}
        public DateTime Reserved {get;set;}
        public Token ReservedFor {get;set;}
        public Token ApprovedBy {get;set;}
        public String Image {get;set;}
        public short ContainerPort {get;set;}
        public String Parameters {get;set;}

        [NotMapped]
        public string Token {
            get { 
                return (ReservedFor != null) ?  ReservedFor.TokenId : tokenId;
            }
            set {
                if (ReservedFor == null) {
                    tokenId = value;
                } else {
                    ReservedFor.TokenId = value;
                }
            }
        }
        private string tokenId = "";
    }
}