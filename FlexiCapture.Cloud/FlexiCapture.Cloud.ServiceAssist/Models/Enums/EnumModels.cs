using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexiCapture.Cloud.ServiceAssist.Models.Enums
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
        AccountOwner = 2,
        Operator = 3,
        Viewer = 4
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

    public enum CommunicationTypes
    {
        Important = 1,
        MonthlyUsePayment = 2,
        PortalUpdatesReleases = 3,
    }

    public enum SubscriptionPlanStates
    {
        Active = 1,
        Disabled = 2
    }

    public enum SubscriptionPlanTypes
    {
        OneTimePurchase = 1,
        Monthly = 2,
        Annual = 3
    }

    public enum SubscriptionPlanUseStates
    {
        Pending = 1,
        Active = 2,
        Disabled = 3,
        Blocked = 4
    }
}