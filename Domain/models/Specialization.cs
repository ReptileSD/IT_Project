using Domain.Logic;

namespace Domain.Models
{
    public class Specialization
    {
        public int Id;
        public string Name;

        public Specialization(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Specialization() : this(0, "") { }

        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Incorrect specialization id.");
            if (string.IsNullOrEmpty(Name))
                return Result.Fail("Incorrect specialization name.");
            return Result.Ok();
        }
    }
}
