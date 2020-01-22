namespace UnitTestProject3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblStrip")]
    public partial class tblStrip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StripID { get; set; }

        public long? NewID { get; set; }

        public long? PaneID { get; set; }

        public long? StripGroupID { get; set; }

        public long? StripTaskID { get; set; }

        public int ExternalSystemID { get; set; }

        public long? TaskID { get; set; }

        public long? StripSubGroupID { get; set; }

        public short? FundementalStripTypeID { get; set; }

        public long? AssetID { get; set; }

        public long? AssetTypeID { get; set; }

        public long? LinkedToID { get; set; }

        [StringLength(6)]
        public string AssetTail { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? InnerStart { get; set; }

        public DateTime? InnerEnd { get; set; }

        public int? Duration { get; set; }

        public int? ActualInnerDuration { get; set; }

        public int? ActualDuration { get; set; }

        public int? EffectiveTime { get; set; }

        public int? IneffectiveTime { get; set; }

        [StringLength(1)]
        public string StripStatus { get; set; }

        [StringLength(10)]
        public string StripStatusCode { get; set; }

        [StringLength(1)]
        public string StripOutcome { get; set; }

        [StringLength(10)]
        public string StripOutcomeCode { get; set; }

        public bool TaskLocked { get; set; }

        [StringLength(50)]
        public string Task { get; set; }

        [StringLength(250)]
        public string Details { get; set; }

        [StringLength(250)]
        public string Details2 { get; set; }

        [StringLength(50)]
        public string SupDetails { get; set; }

        [StringLength(20)]
        public string Callsign { get; set; }

        public long? WaypointToID { get; set; }

        public long? WaypointFromID { get; set; }

        public int? StripColour { get; set; }

        [StringLength(11)]
        public string HoursCode { get; set; }

        public long? HoursCodeID { get; set; }

        [StringLength(1)]
        public string HoursCodeStatus { get; set; }

        public bool MissionReleased { get; set; }

        public long MinWeatherState { get; set; }

        public long MaxWeatherState { get; set; }

        public int? AuthLevel { get; set; }

        public int AuthGroupID { get; set; }

        public long? UserStatus1 { get; set; }

        public long? UserStatus2 { get; set; }

        public int? POB { get; set; }

        public int? DisplayRow { get; set; }

        public int? DisplayHeight { get; set; }

        public int DutyFactorMins { get; set; }

        public int DutyFactorPer { get; set; }

        public int RestFactorMins { get; set; }

        public int RestFactorPer { get; set; }

        public int PaxOn { get; set; }

        public int PaxOff { get; set; }

        public decimal? FreightOn { get; set; }

        public decimal? FreightOff { get; set; }

        public decimal? FuelOn { get; set; }

        public decimal? FuelOff { get; set; }

        public int FuelUnits { get; set; }

        public int FreightUnits { get; set; }

        public DateTime? LastUpdated { get; set; }

        [StringLength(10)]
        public string LoginName { get; set; }

        public int Day { get; set; }

        public int Night { get; set; }
    }
}
