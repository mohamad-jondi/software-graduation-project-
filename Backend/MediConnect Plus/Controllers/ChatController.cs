using Microsoft.AspNetCore.Mvc;
using Domain.IServices;
using Domain.DTOs.Chats;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("GetChatsByUser/{username}")]
    public async Task<ActionResult<IEnumerable<ChatForShowingFromTheOutsideDTO>>> GetChatsByUserUsername(string username)
    {
        var chats = await _chatService.GetChatsByUserUsername(username);
        if (chats != null)
            return Ok(chats);
        return BadRequest("No chats found for the user.");
    }

    [HttpGet("GetChatsByUser/{username}/Browse/{ChatID}")]
    public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> BrowseMessage(int ChatID)
    {
        var message = await _chatService.BrowseChat(ChatID);
        if (message != null)
            return Ok(message);
        return BadRequest("Message not found.");
    }

    [HttpPost("SendMessage")]
    public async Task<ActionResult<ChatMessageDTO>> SendMessage(string senderUsername, string receiverUsername, [FromBody] CreateChatMessageDTO chatMessageDTO)
    {
        var message = await _chatService.SendMessage(senderUsername, receiverUsername, chatMessageDTO);
        if (message != null)
            return Ok(message);
        return BadRequest("Failed to send message.");
    }

    [HttpDelete("DeleteMessage/{messageId}")]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        var result = await _chatService.DeleteMessage(messageId);
        if (result)
            return Ok("Message deleted successfully.");
        return BadRequest("Failed to delete message.");
    }
}
