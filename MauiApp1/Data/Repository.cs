using SQLite;
using System.Linq;

namespace MedsTimer.Data
{
    public class Repository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public Repository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public void Init()
        {
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Notifications>();
            conn.CreateTable<Prescription>();
            conn.CreateTable<Cough_Cold_Pain>();
            conn.CreateTable<Members>();
        }
        public List<Prescription> GetAllPrescriptions()
        {
            Init();
            return conn.Table<Prescription>().ToList();
        }
        public List<Cough_Cold_Pain> GetAllCough_Cold_Pain()
        {
            Init();
            return conn.Table<Cough_Cold_Pain>().ToList();
        }
        public List<Prescription> GetPrescriptions(int id)
        {
            Init();
            return conn.Table<Prescription>().Where(x => x.MemberId == id).ToList();

        }
        public List<Cough_Cold_Pain> GetCough_Cold_pain(int id)
        {
            Init();
            return conn.Table<Cough_Cold_Pain>().Where(x => x.MemberId == id).ToList();

        }
        public bool AddMedication(Prescription item)
        {
            Init();
            try
            {
                conn.Insert(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddMedication(Cough_Cold_Pain item)
        {
            Init();
            try
            {
                conn.Insert(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Prescription getPrescriptions(int item)
        {
            Init();
            return conn.Table<Prescription>().FirstOrDefault(x => x.MedicationId == item);
        }
        public Cough_Cold_Pain getCough_Cold_Pain(int item)
        {
            Init();
            return conn.Table<Cough_Cold_Pain>().FirstOrDefault(x => x.MedicationId == item);
        }
        public void DeletePrescription(int id)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Prescription { MedicationId = id });
        }
        public void DeleteCold_Cough_Pain(int id)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Cough_Cold_Pain { MedicationId = id });
        }
        public bool UpdatePrescription(Prescription prescription)
        {
            Init();
            try
            {
                conn.Update(prescription);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool UpdateCough_Cold_Pain(Cough_Cold_Pain Cough_Cold_Pain)
        {
            Init();
            try
            {
                conn.Update(Cough_Cold_Pain);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public List<Members> getMembers()
        {
            Init();
            return conn.Table<Members>().ToList();
        }
        public int addMember(Members members)
        {
            Init();
            conn.Insert(members);
            return conn.Table<Members>().Last().MemberId;
        }
        public Members getMember(int id)
        {
            Init();
            return conn.Table<Members>().FirstOrDefault(x => x.MemberId == id);
        }
        public void DeleteMember(int id)
        {
            Init();
            conn.Delete(new Members { MemberId = id});
        }
        public int AddNotification()
        {
            Init();
            var placeholder = new Notifications { Notification_Title = "NotiTitle" };
            conn.Insert(placeholder);
            return conn.Table<Notifications>().Last().Notification_Id;
        }

    }
}
