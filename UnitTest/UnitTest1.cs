using DevExpress.Data.Controls.ExpressionEditor.Native;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void AddMember()
        {
            var member = new Members();
            member.MemberFName = "Ethan";
            member.MemberLName = "Strain";
            member.MemberAge = "52";
            using (var conn = new SQLiteConnection("DataSource=:memory"))
            {
                conn.CreateTable<Members>();
                var success = Success();
                bool Success()
                {
                    try
                    {
                        conn.Insert(member);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                Assert.True(success);
            }
        }
        [Fact]
        public void AddMedication()
        {
            var member = new Members();
            member.MemberFName = "Ethan";
            member.MemberLName = "Strain";
            member.MemberAge = "52";
            var memberid = 0;
            using(var conn = new SQLiteConnection("DataSource=:memory"))
            {
                conn.CreateTable<Members>();
                conn.Insert(member);
                memberid = conn.Table<Members>().Last().MemberId;
                var medication = new Prescription();
                medication.MedicationName = "TestMeds";
                medication.MemberId = memberid;
                medication.NumberofTimesaDay = 1;
                medication.Dose1 = "09:30 AM";
                conn.CreateTable<Prescription>();
                var success = Success();
                bool Success(){
                    try
                    {
                        conn.Insert(medication);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                Assert.True(success);
            }
        }
        [Fact]
        public void DeleteMedication()
        {
            using (var conn = new SQLiteConnection("DataSource=:memory"))
            {
                var member = new Members();
                member.MemberFName = "Ethan";
                member.MemberLName = "Strain";
                member.MemberAge = "52";
                var memberid = 0;
                conn.CreateTable<Members>();
                conn.Insert(member);
                memberid = conn.Table<Members>().Last().MemberId;
                var medication = new Prescription();
                medication.MedicationName = "TestMeds";
                medication.MemberId = memberid;
                medication.NumberofTimesaDay = 1;
                medication.Dose1 = "09:30 AM";
                conn.CreateTable<Prescription>();
                conn.Insert(medication);
                var success = Success();
                bool Success()
                {
                    try
                    {
                        conn.Delete(new Prescription { MemberId = memberid });
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                
                Assert.True(success);
            }
        }
        
        [Fact]
        public void CatchNullEntry()
        {
            var Member = new Members();
            Member.MemberFName = "";
            Member.MemberLName = null;
            Member.MemberAge = "20";
            var success = Success();
            bool Success()
            {
                var fname = Member._NotNull(Member.MemberFName);
                var lname = Member._NotNull(Member.MemberLName);    
                var age = Member._NotNull(Member.MemberAge);
                if(fname == false || lname == false || age == false)
                {
                    return false;
                }
                else { return true; }
                
            }
            Assert.True(!success);
        }
    }
}