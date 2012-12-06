/// <summary>
/// Summary description for Interfaces
/// </summary>
public class Interfaces
{
    public interface IPatientControl : INamedControl
    {
        void LoadPatient(int patientID);
        void Reset();
    }

    public interface INamedControl
    {
        string Name
        {
            get;
        }
    }

    public enum GridColorSchemes
    {
        Yellow,
        Brick,
        Green,
        Orange,
        Blue,
        Monochrome
    }

    public interface IColouredGrid
    {
        GridColorSchemes GridColorScheme
        {
            get;
        }
    }

    public delegate string GetKeySynonymDelegate(string key);

    public interface IValueHolder
    {
        object this[string valueName]
        {
            get;
        }

        GetKeySynonymDelegate GetKeySynonym
        {
            set;
        }
    }
}
