using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net.Mime;

namespace SMSOverEmail
{
    /// <summary>
    /// An enum organizing the list of carrier supported by the API
    /// </summary>
    public enum Carrier
    {
        ATT,
        AirfireMobile,
        AlaskaCommunications,
        Alltel,
        Ameritech,
        AppalachianWireless,
        ATTEnterprisePaging,
        BellSouth,
        BluegrassCellular,
        BlueskyCommunications,
        Boost,
        CBeyond,
        CellCom,
        CellularSouth,
        CentennialWireless,
        CharitonValleyWireless,
        CincinnatiBell,
        Cingular,
        Claro,
        Cleartalk,
        Cricket,
        CSpireWireless,
        EdgeWireless,
        ElementMobile,
        Esendex,
        GeneralCommunications,
        GoldenStateCellular,
        HawaiianTelcomWireless,
        Helio,
        iWirelessSprintAffiliate,
        iWirelessTMobileAffiliate,
        Kajeet,
        LongLines,
        MetroPCS,
        Nextech,
        Nextel,
        PagePlusCellular,
        PanaceaMobile,
        PocketWireless,
        QwestWireless,
        RedPocketMobile,
        Rogers,
        RoutoMessaging,
        SimpleMobile,
        SouthCentralCommunications,
        Southernlinc,
        Sprint,
        StraightTalkATT,
        StraightTalkTMobile,
        StraightTalkVerizon,
        SyringaWireless,
        Teleflip,
        TelusMobility,
        TMobile,
        TracFone,
        Unicel,
        USAMobility,
        USCellular,
        Verizon,
        Viaero,
        VirginMobile,
        Voyager,
        WestCentralWireless,
        XITCommunications
    }

    /// <summary>
    /// Refactors code from the original SMSOverEmail project as extensions to the MailMessage class
    /// Original project: http://smsoveremail.codeplex.com/
    /// This was re-structured for two reasons:
    /// 1) Making it an extension worked better with some existing code
    /// 2) Simplify presenting the list of options in MVC's razor
    /// This implementation is built from the MailExtesnsions class and is
    /// </summary>
    public class CarrierInfo
    {
        #region Instance Fields & Methods

        /// <summary>
        /// Constructor that sets all fields of the Carrier info object
        /// </summary>
        /// <param name="carrier"></param>
        /// <param name="name"></param>
        /// <param name="template"></param>
        public CarrierInfo(Carrier carrier, string name, string template)
        {
            this.Carrier = carrier;
            this.Name = name;
            this.Template = template;
        }

        public Carrier Carrier { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }

        #endregion

        #region Static Fields & Methods

        /// <summary>
        /// For formatting natural phone number strings into plain blocks of digits
        /// </summary>
        static Regex phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

        /// <summary>
        /// The dictionary of email addresses to use for each carrier.  The format is #@Address.com.
        /// </summary>
        static Dictionary<Carrier, CarrierInfo> _CarrierMapping = new Dictionary<Carrier, CarrierInfo>
        {
             { Carrier.ATT , new CarrierInfo(Carrier.ATT, "AT&T Wireless", "#@txt.att.net") },
             { Carrier.AirfireMobile , new CarrierInfo(Carrier.AirfireMobile, "Verizon Wireless", "#@vtext.com") },
             { Carrier.AlaskaCommunications , new CarrierInfo(Carrier.AlaskaCommunications, "T-Mobile", "#@tmomail.net") },
             { Carrier.Alltel , new CarrierInfo(Carrier.Alltel, "Sprint PCS", "#@messaging.sprintpcs.com") },
             { Carrier.Ameritech , new CarrierInfo(Carrier.Ameritech, "Virgin Mobile", "#@vmobl.com") },
             { Carrier.AppalachianWireless , new CarrierInfo(Carrier.AppalachianWireless, "US Cellular", "#@email.uscc.net") },
             { Carrier.ATTEnterprisePaging , new CarrierInfo(Carrier.ATTEnterprisePaging, "Nextel Wireless", "#@messaging.nextel.com") },
             { Carrier.BellSouth , new CarrierInfo(Carrier.BellSouth, "Boost Mobile", "#@myboostmobile.com") },
             { Carrier.BluegrassCellular , new CarrierInfo(Carrier.BluegrassCellular, "Alltell", "#@message.alltel.com") },
             { Carrier.BlueskyCommunications , new CarrierInfo(Carrier.BlueskyCommunications, "C Beyond", "#@cbeyond.sprintpcs.com") },
             { Carrier.Boost , new CarrierInfo(Carrier.Boost, "General Communications Inc.", "#@mobile.gci.net") },
             { Carrier.CBeyond , new CarrierInfo(Carrier.CBeyond, "Bluesky Communications", "#@psms.bluesky.as") },
             { Carrier.CellCom , new CarrierInfo(Carrier.CellCom, "Golden State Cellular", "#@gscsms.com") },
             { Carrier.CellularSouth , new CarrierInfo(Carrier.CellularSouth, "Telus Mobility", "#@msg.telus.com") },
             { Carrier.CentennialWireless , new CarrierInfo(Carrier.CentennialWireless, "Rogers Wireless", "#@pcs.rogers.com") },
             { Carrier.CharitonValleyWireless , new CarrierInfo(Carrier.CharitonValleyWireless, "Cincinnati Bell", "#@gocbw.com") },
             { Carrier.CincinnatiBell , new CarrierInfo(Carrier.CincinnatiBell, "Hawaiian Telcom Wireless", "#@hawaii.sprintpcs.com") },
             { Carrier.Cingular , new CarrierInfo(Carrier.Cingular, "i wireless (T-Mobile Affiliate)", "#.iws@iwspcs.net") },
             { Carrier.Claro , new CarrierInfo(Carrier.Claro, "i-wireless (Sprint Affiliate)", "#@iwirelesshometext.com") },
             { Carrier.Cleartalk , new CarrierInfo(Carrier.Cleartalk, "Claro", "#@vtexto.com") },
             { Carrier.Cricket , new CarrierInfo(Carrier.Cricket, "Helio", "#@myhelio.com") },
             { Carrier.CSpireWireless , new CarrierInfo(Carrier.CSpireWireless, "Pocket Wireless", "#@sms.pocket.com") },
             { Carrier.EdgeWireless , new CarrierInfo(Carrier.EdgeWireless, "Voyager", "#@text.voyagermobile.com") },
             { Carrier.ElementMobile , new CarrierInfo(Carrier.ElementMobile, "Airfire Mobile", "#@sms.airfiremobile.com") },
             { Carrier.Esendex , new CarrierInfo(Carrier.Esendex, "Alaska Communications", "#@msg.acsalaska.com") },
             { Carrier.GeneralCommunications , new CarrierInfo(Carrier.GeneralCommunications, "Ameritech", "#@paging.acswireless.com") },
             { Carrier.GoldenStateCellular , new CarrierInfo(Carrier.GoldenStateCellular, "AT&T Enterprise Paging", "#@page.att.net") },
             { Carrier.HawaiianTelcomWireless , new CarrierInfo(Carrier.HawaiianTelcomWireless, "BellSouth", "#@bellsouth.cl") },
             { Carrier.Helio , new CarrierInfo(Carrier.Helio, "Bluegrass Cellular", "#@sms.bluecell.com") },
             { Carrier.iWirelessSprintAffiliate , new CarrierInfo(Carrier.iWirelessSprintAffiliate, "Cellcom", "#@cellcom.quiktxt.com") },
             { Carrier.iWirelessTMobileAffiliate , new CarrierInfo(Carrier.iWirelessTMobileAffiliate, "Cellular South", "#@csouth1.com") },
             { Carrier.Kajeet , new CarrierInfo(Carrier.Kajeet, "Chariton Valley Wireless", "#@sms.cvalley.net") },
             { Carrier.LongLines , new CarrierInfo(Carrier.LongLines, "Cingular", "#@cingular.com") },
             { Carrier.MetroPCS , new CarrierInfo(Carrier.MetroPCS, "Cleartalk", "#@sms.cleartalk.us") },
             { Carrier.Nextech , new CarrierInfo(Carrier.Nextech, "Cricket", "#@sms.mycricket.com") },
             { Carrier.Nextel , new CarrierInfo(Carrier.Nextel, "C Spire Wireless", "#@cspire1.com") },
             { Carrier.PagePlusCellular , new CarrierInfo(Carrier.PagePlusCellular, "Edge Wireless", "#@sms.edgewireless.com") },
             { Carrier.PanaceaMobile , new CarrierInfo(Carrier.PanaceaMobile, "Element Mobile", "#@SMS.elementmobile.net") },
             { Carrier.PocketWireless , new CarrierInfo(Carrier.PocketWireless, "Esendex", "#@echoemail.net") },
             { Carrier.QwestWireless , new CarrierInfo(Carrier.QwestWireless, "Kajeet", "#@mobile.kajeet.net") },
             { Carrier.RedPocketMobile , new CarrierInfo(Carrier.RedPocketMobile, "LongLines", "#@text.longlines.com") },
             { Carrier.Rogers , new CarrierInfo(Carrier.Rogers, "MetroPCS", "#@mymetropcs.com") },
             { Carrier.RoutoMessaging , new CarrierInfo(Carrier.RoutoMessaging, "Nextech", "#@sms.nextechwireless.com") },
             { Carrier.SimpleMobile , new CarrierInfo(Carrier.SimpleMobile, "Page Plus Cellular", "#@vtext.com") },
             { Carrier.SouthCentralCommunications , new CarrierInfo(Carrier.SouthCentralCommunications, "Qwest Wireless", "#@qwestmp.com") },
             { Carrier.Southernlinc , new CarrierInfo(Carrier.Southernlinc, "Red Pocket Mobile", "#@txt.att.net") },
             { Carrier.Sprint , new CarrierInfo(Carrier.Sprint, "Simple Mobile", "#@smtext.com") },
             { Carrier.StraightTalkATT , new CarrierInfo(Carrier.StraightTalkATT, "Southernlinc", "#@page.southernlinc.com") },
             { Carrier.StraightTalkTMobile , new CarrierInfo(Carrier.StraightTalkTMobile, "South Central Communications", "#@rinasms.com") },
             { Carrier.StraightTalkVerizon , new CarrierInfo(Carrier.StraightTalkVerizon, "Straight Talk (AT&T SIM)", "#@txt.att.net") },
             { Carrier.SyringaWireless , new CarrierInfo(Carrier.SyringaWireless, "Straight Talk (T-Mobile SIM)", "#@mmst5.tracfone.com") },
             { Carrier.Teleflip , new CarrierInfo(Carrier.Teleflip, "Straight Talk (Verizon SIM)", "#@vtext.com") },
             { Carrier.TelusMobility , new CarrierInfo(Carrier.TelusMobility, "Syringa Wireless", "#@rinasms.com") },
             { Carrier.TMobile , new CarrierInfo(Carrier.TMobile, "Teleflip", "#@teleflip.com") },
             { Carrier.TracFone , new CarrierInfo(Carrier.TracFone, "Unicel", "#@utext.com") },
             { Carrier.Unicel , new CarrierInfo(Carrier.Unicel, "USA Mobility", "#@usamobility.net") },
             { Carrier.USAMobility , new CarrierInfo(Carrier.USAMobility, "Viaero", "#@viaerosms.com") },
             { Carrier.USCellular , new CarrierInfo(Carrier.USCellular, "West Central Wireless", "#@sms.wcc.net") },
             { Carrier.Verizon , new CarrierInfo(Carrier.Verizon, "XIT Communications", "#@sms.xit.net") },
             { Carrier.Viaero , new CarrierInfo(Carrier.Viaero, "TracFone", "#@mmst5.tracfone.com") },
             { Carrier.VirginMobile , new CarrierInfo(Carrier.VirginMobile, "Centennial Wireless", "#@cwemail.com") },
             { Carrier.Voyager , new CarrierInfo(Carrier.Voyager, "Panacea Mobile", "#@api.panaceamobile.com") },
             { Carrier.WestCentralWireless , new CarrierInfo(Carrier.WestCentralWireless, "RoutoMessaging", "#@email2sms.routomessaging.com") },
             { Carrier.XITCommunications , new CarrierInfo(Carrier.XITCommunications, "Appalachian Wireless", "#@awsms.com") }
        };

        /// <summary>
        /// Retrieves a read-only list of all carriers declared
        /// </summary>
        public static IEnumerable<CarrierInfo> Carriers
        {
            get { return _CarrierMapping.Values; }
        }

        /// <summary>
        /// Public getter for the name of the carrier from the enum
        /// </summary>
        /// <param name="carrier"></param>
        /// <returns></returns>
        public static string GetCarrierName(Carrier carrier)
        {
            return _CarrierMapping[carrier].Name;
        }

        /// <summary>
        /// Public getter for the address template from the enum
        /// </summary>
        /// <param name="carrier"></param>
        /// <returns></returns>
        public static string GetCarrierAddress(Carrier carrier)
        {
            return _CarrierMapping[carrier].Template;
        }

        /// <summary>
        /// Maps a carrier and phone number into an e-mail
        /// </summary>
        /// <param name="carrier"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string GetSMSAddress(string phoneNumber, Carrier carrier)
        {
            // Ensure phone is formatted without spaces or hyphens
            string formattedPhoneNumber = phoneRegex.Replace(phoneNumber, "$1$2$3");
            // Retrieve the template for the given carrier
            string carrierAddressTemplate = GetCarrierAddress(carrier);
            // Load up the template and return
            return carrierAddressTemplate.Replace("#", formattedPhoneNumber);
        }

        #endregion
    }
}
