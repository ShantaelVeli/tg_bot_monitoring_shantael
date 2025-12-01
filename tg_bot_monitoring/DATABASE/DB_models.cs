namespace DataBase.Models
{
    public class Users_chat
    {
        public int id {get; set;}
        public long chat_id {get; set;}
        public List<Sshkey> Sshkeys {get;set;}= new ();
    }

    public class Sshkey
    {
        public int id {get; set;}
        public string ssh {get; set;} = "";
        public int chat_id_key {get;set;}
        public Users_chat? Users_chat {get; set;}

    }
}