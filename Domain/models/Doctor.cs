using Domain.Logic;

namespace Domain.Models
{
    public class Doctor
    {
        public int Id;
        public string Fullname;
        public Specialization Specialization;


        public Doctor(int id, string fullname, Specialization specialization)
        {
            Id = id;
            Fullname = fullname;
            Specialization = specialization;
        }

        public Doctor() : this(-1, "", new Specialization()) { }

        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Incorrect doctor id.");
            if (string.IsNullOrEmpty(Fullname))
                return Result.Fail("Incorrect doctor fullname.");
            if (Specialization.IsValid().isFailure)
                return Result.Fail("Incorrect specialization.");
            return Result.Ok();


        }
    }
}
