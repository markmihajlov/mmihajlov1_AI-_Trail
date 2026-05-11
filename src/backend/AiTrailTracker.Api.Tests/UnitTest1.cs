// Modified by AI on 05/11/2026. Edit #1.
using AiTrailTracker.Api.Models;
using AiTrailTracker.Api.Services;
using AiTrailTracker.Api.Storage;

namespace AiTrailTracker.Api.Tests;

public class SmokeTests
{
    [Fact]
    public void InMemoryStorage_CanBeInstantiated()
    {
        var storage = new InMemoryStorage();
        Assert.NotNull(storage);
    }

    [Fact]
    public async Task ParticipantService_GetOrCreate_ReturnsNewParticipant()
    {
        var storage = new InMemoryStorage();
        var service = new ParticipantService(storage);

        var participant = await service.GetOrCreateParticipantAsync("test-user-1", "Test User", "test@example.com");

        Assert.NotNull(participant);
        Assert.Equal("test-user-1", participant.UserId);
        Assert.Equal("Test User", participant.DisplayName);
        Assert.Equal(BadgeLevel.Greenhorn, participant.CurrentLevel);
    }
}
