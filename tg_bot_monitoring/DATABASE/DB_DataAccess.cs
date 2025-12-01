using DataBase.Models;
using DataBase.Context;

namespace DataBase.DataAccess{
    public class DataAccess{
        private readonly ApplicationContext _db;

        public DataAccess(ApplicationContext db)
        {
            _db = db;
        }

        public async Task PushDataChat(int id)
        {
            Users_chat newChat = new();
            newChat.chat_id = id;
            _db.users_chat.Add(newChat);
            await _db.SaveChangesAsync();
        }

        public async Task PushDataSSh(int id, string sshs)
        {
            Sshkey newSshkey = new();
            newSshkey.chat_id_key = id;
            newSshkey.ssh = sshs;
            _db.sshkeys.Add(newSshkey);
            await _db.SaveChangesAsync();
        }
    }
}