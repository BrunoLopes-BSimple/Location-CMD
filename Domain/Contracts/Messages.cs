namespace Domain.Contracts;

public record DataForLocation(Guid meetingId, string description);

public record LocationCreatedForMeeting(Guid LocationId, Guid MeetingId);
