using Isu.Extra.Tools;

namespace Isu.Extra.Models
{
    public record OgnpGroupName
    {
        private const int LengthOfGroupName = 6;

        public OgnpGroupName(string nameOfGroup)
        {
            ValidationCheck(nameOfGroup);
            NameOfGroup = nameOfGroup;
            NameOfOGNPCourse = nameOfGroup.Substring(0, 3);
            NumberOfFlow = int.Parse(nameOfGroup[3].ToString());
            NumberOfGroup = int.Parse(nameOfGroup[5].ToString());
        }

        public string NameOfGroup { get; }
        public string NameOfOGNPCourse { get; }
        public int NumberOfFlow { get; }
        public int NumberOfGroup { get; }

        public static bool ValidationCheck(string nameOfGroup)
        {
            string.IsNullOrWhiteSpace(nameOfGroup);

            if (nameOfGroup.Length != LengthOfGroupName)
                throw new OgnpNameException("The length of group name should be 6", nameOfGroup.Length);

            if (!(char.IsDigit(nameOfGroup[3]) && char.IsDigit(nameOfGroup[5]) && char.IsLetter(nameOfGroup[0]) && char.IsLetter(nameOfGroup[1]) && char.IsLetter(nameOfGroup[2])))
                throw new OgnpNameException("The OGNP group name should be LLLN.N");

            if (nameOfGroup[4] != '.')
                throw new OgnpNameException("The OGNP group name should be LLLN.N");

            return true;
        }
    }
}
