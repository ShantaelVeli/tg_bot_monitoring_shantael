using Microsoft.EntityFrameworkCore;
using DataBase.Models;

namespace DataBase.modelBuilder
{
    public static class TaskKeyBuilder{

        public static ModelBuilder AddHasKey(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users_chat>().HasAlternateKey(u => u.chat_id);
            modelBuilder.Entity<Sshkey>().HasAlternateKey(u => u.ssh);

            return modelBuilder;
        }
        
        public static ModelBuilder AddForignKey(this ModelBuilder modelBuilder){
            modelBuilder.Entity<Users_chat>(entity =>{
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd(); 
                entity.Property(e => e.chat_id).IsRequired(); 

                 entity.HasMany(e => e.Sshkeys)
                    .WithOne(e => e.Users_chat)  // Обратная навигация
                    .HasForeignKey(e => e.chat_id_key)  // Внешний ключ в Sshkey
                    .OnDelete(DeleteBehavior.Cascade);  
            });

            modelBuilder.Entity<Sshkey>(entity =>
            {
                entity.HasKey(e => e.id);  // Первичный ключ
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.ssh).IsRequired().HasMaxLength(500);  // Обязательная строка SSH
                entity.Property(e => e.chat_id_key).IsRequired();  // Обязательный внешний ключ
            });
            return modelBuilder;
        }
    }
}