using Domain.Logic;

namespace Domain.Models
{
    public class Doctor
    {
        public int Id;
        public string Fullname;
        public int SpecializationId;

        public Doctor(int id, string fullname, int specializationId)
        {
            Id = id;
            Fullname = fullname;
            SpecializationId = specializationId;
        }

        public Doctor() : this(0, "default", 0) { }
        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Incorrect doctor id.");
            if (string.IsNullOrEmpty(Fullname))
                return Result.Fail("Incorrect doctor fullname.");
            if (SpecializationId < 0)
                return Result.Fail("Incorrect specialization id");
            return Result.Ok();


        }
    }
}
