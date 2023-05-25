using Humanizer.Localisation.TimeToClockNotation;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Data
{
    public class Animal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId {get; set;}
        public string? Name { get; set; }

        public string? Type { get; set; }

        public int Leg { get; set; }

        public int Arm { get; set; }

        public int Head { get; set; }

        public int Eyes { get; set; }

        public int Tail { get; set; }

        //public Animal(string name, string type, int leg, int arm, int head, int eyes, int tail)
        //{
        //    Name = name;
        //    Type = type;
        //    Leg = leg;
        //    Arm = arm;
        //    Head = head;
        //    Eyes = eyes;
        //    Tail = tail;
        //}
    }
}
