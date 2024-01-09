using System.Collections.ObjectModel;

namespace MedsTimer.Views.Reporting;

public partial class Report : ContentPage
{
	int ReportId;
	public Report(int reportId)
	{
		InitializeComponent();
		ReportId = reportId;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if(ReportId == 1) {
            Report1();
        }
        if (ReportId == 2)
        {
            Report2();
        }
        if (ReportId == 3)
        {
            Report3();
        }
    }
    private void Back_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new Reports(), false);
    }
    private class mbrCount
    {
        public string MemberName { get; set; }
        public int Count { get; set; }
    }
    public void Report1()
    {
       var source = new ObservableCollection<mbrCount>();
        var members = App.Repository.getMembers();
        foreach (var member in members)
        {
            var mbr = new mbrCount();
                mbr.MemberName = member.MemberFName;
            var presc = App.Repository.GetPrescriptions(member.MemberId).Count();
            var CCP = App.Repository.GetCough_Cold_pain(member.MemberId).Count();
                mbr.Count = presc+CCP;
            source.Add(mbr);
        }
        _Medications.ItemsSource = source;
        _Report1.IsVisible = true;
    }
    private void Report2()
    {
        var prescount = App.Repository.GetAllPrescriptions().Count();
        _pres.Text = prescount.ToString();
        var ccpCount = App.Repository.GetAllCough_Cold_Pain().Count();
        _ccp.Text = ccpCount.ToString();
        _Report2.IsVisible = true;
    }
    private void Report3()
    {
        var members = App.Repository.getMembers();
        MemberPicker.ItemsSource = members;
        MemberPicker.SelectedIndex = 0;
        _report3Title.Text = $"{members[MemberPicker.SelectedIndex].MemberFName}'s Prescriptions";
        _Report3.IsVisible = true;
    }

    private void MemberPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var members = App.Repository.getMembers();
        _report3Title.Text = $"{members[MemberPicker.SelectedIndex].MemberFName}'s Prescriptions";
        _report3Count.Text = App.Repository.GetPrescriptions(members[MemberPicker.SelectedIndex].MemberId).Count().ToString();
        _report3results.IsVisible = true;
    }
}