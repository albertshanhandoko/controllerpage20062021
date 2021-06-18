using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerPage.Library
{
    class Data_History
    {
        public Data_History(int id, string downloadedBy, string approvedBy, DateTime downloaded_date, string filename)
        {
            set(id, downloadedBy, approvedBy, downloaded_date, filename);
        }
        public int Id { set; get; }
        public string DownloadedBy { set; get; }
        public string ApprovedBy { set; get; }
        public DateTime Downloaded_date { set; get; }
        public string FileName { set; get; }

        public void set(int id, string downloadedBy, string approvedBy, DateTime downloaded_date, string filename)
        {
            Id = id;
            DownloadedBy = downloadedBy;
            ApprovedBy = approvedBy;
            Downloaded_date = downloaded_date;
            FileName = filename;
        }
    }
}
