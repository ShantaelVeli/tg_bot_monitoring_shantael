using DataBase.Models;
using DataBase.Context;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataBase.DataAccess
{
    public class DataAccess
    {
        private readonly ApplicationContext _db;

        public DataAccess(ApplicationContext db)
        {
            _db = db;
        }

        public async Task PushDataChat(long id)
        {
            bool exists = await _db.users_chat.AnyAsync(u => u.chat_id == id);

            if (!exists)
            {
                Users_chat newChat = new();
                newChat.chat_id = id;
                _db.users_chat.Add(newChat);
                await _db.SaveChangesAsync();
                Log.Information("chat_id: " + id + " added to the database");
            }
            else
            {
                Log.Information("Chat_id: " + id + " already exists in the database");
            }
        }

        public async Task PushDataSSh(Message msg)
        {
            bool exists = await _db.sshkeys.AnyAsync(u => u.ssh == msg.Text);

            if (!exists)
            {
                var chat = await _db.users_chat.Where(u => u.chat_id == msg.Chat.Id).FirstOrDefaultAsync();
                Sshkey newSshkey = new();
                if (chat != null)
                {
                    newSshkey.chat_id_key = chat.id;
                    if (msg.Text != null)
                        newSshkey.ssh = msg.Text;
                    _db.sshkeys.Add(newSshkey);
                    await _db.SaveChangesAsync();
                    Log.Information("sshkey " + msg.Text + " added to the database");
                }
                else
                {
                    Log.Error("This chat "+ msg.Chat.Id+" was not found");
                }
            }
            else
            {
                Log.Information("sshkey " + msg.Text + " already exists in the database");
            }

        }
    }
}