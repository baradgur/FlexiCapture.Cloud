using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.Portal.Api.Models.Enums
{
    public enum ServiceTypes
    {
        Single = 1,
        Batch = 2,
        FTP = 3,
        Email = 4
    }

    public enum UserLoginStateTypes
    {
        Active = 1,
        WaitConfirm = 2,
        Locked = 3
    }

    public enum SubscribeStates
    {
        Subscribe = 1,
        Cancel = 2
    }

    public enum UserRoleTypes
    {
        Administrator = 1,
        Operator = 2,
        Viewer = 3
    }

    public enum DocumentStates
    {
        Uploaded = 1,
        Processing = 2,
        Completed = 3,
        Error = 4
    }

    public enum DocumentTypes
    {
        PDF = 1,
        JPG = 2,
        PNG = 3,
        Word = 4,
        Excel = 5,
        PowerPoint = 6,
        DBF = 7,
        CSV = 8,
        Text = 9,
        RTF = 10,
        ZIP = 12,
        BMP = 13
    }
}