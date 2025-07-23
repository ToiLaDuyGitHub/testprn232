﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        // UserId có thể null cho guest booking
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        public string BookingCode { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty; // Temporary, Confirmed, Cancelled, Expired
        public String? PaymentStatus { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime? ExpirationTime { get; set; }

        // Thông tin hành khách (bắt buộc)
        public string PassengerName { get; set; } = string.Empty;
        public string PassengerPhone { get; set; } = string.Empty;
        public string PassengerEmail { get; set; } = string.Empty;
        public string? PassengerIdCard { get; set; }
        public DateTime? PassengerDateOfBirth { get; set; }

        // Thông tin liên hệ (cho guest booking)
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        // Navigation properties
        public ICollection<SeatSegment> SeatSegments { get; set; } = new List<SeatSegment>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // Helper properties
        public bool IsGuestBooking => !UserId.HasValue;
        public string ContactInfo => IsGuestBooking ?
            $"{ContactName} - {ContactPhone}" :
            $"{User?.FullName} - {User?.Phone}";
    }
}