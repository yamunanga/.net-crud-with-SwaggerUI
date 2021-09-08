using System.ComponentModel.DataAnnotations;
namespace newApp.models
{
    public class Student
    {   
        public int StudentId{get;set;}
        [Required]
        public string FirstName{get;set;}
        [Required]
        public string LastName{get;set;}

        public string City{get;set;}
        
        public string State{get;set;}
    }
}