using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System;
using System.Linq;

namespace RegymBot.Data.DbInitialize
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.StaticMessages.Any())
            {
                return; // DB has been seeded
            }

            var staticMessages = new StaticMessageEntity[]
            {
                new StaticMessageEntity
                {
                    Page = BotPage.StartPage,
                    Message = "Виберіть:"
                },
                new StaticMessageEntity
                {
                    Page = BotPage.MassagePage,
                    Message = "В наших залах в PSHKN центр и ТРЦ Вавилон вы можете посетить массажные кабинеты, в которых вам помогут расслабиться после тренировки с помощью пятнадцати видов массажа."
                },
                new StaticMessageEntity
                {
                    Page = BotPage.PricePage,
                    Message = "Прайс лист тренувань:"
                },
                new StaticMessageEntity
                {
                    Page = BotPage.LeaveFeedbackPage,
                    Message = "Расскажите, что вам понравилось и как нам стать лутше для вас?"
                },
                new StaticMessageEntity
                {
                    Page = BotPage.SelectClubPage,
                    Message = "Выберите клуб:"
                },
                new StaticMessageEntity
                {
                    Page = BotPage.SolariumPage,
                    Message = "В наших залах в Аполло и ТРЦ Вавилон вы можете посетить солярий."
                },
                new StaticMessageEntity
                {
                    Page = BotPage.Club_Apollo,
                    Message = "Телефон Аполло\n+380999999999\n\"Адрес:\nул. Хмельницкого 68\nРасписание:\nПН - СБ: 08 - 21\nВС: 9 - 18\""
                },
                new StaticMessageEntity
                {
                    Page = BotPage.Club_Vavylon,
                    Message = "Телефон Вавилон\n+380999999999\n\"Адрес:\nул. Хмельницкого 68\nРасписание:\nПН - СБ: 08 - 21\nВС: 9 - 18\""
                },
                new StaticMessageEntity
                {
                    Page = BotPage.Club_Pshkn,
                    Message = "Телефон PSHKN\n+380999999999\n\"Адрес:\nул. Хмельницкого 68\nРасписание:\nПН - СБ: 08 - 21\nВС: 9 - 18\""
                },
                new StaticMessageEntity
                {
                    Page = BotPage.CategoryPage,
                    Message = "Виберіть категорію:"
                }
            };

            dbContext.StaticMessages.AddRange(staticMessages);

            var prices = new PriceEntity[]
            {
                new PriceEntity
                {
                    PriceName = "Позиція 2 (опис 2)",
                    Price = 120,
                    PriceType = PriceItem.Training
                },
                new PriceEntity
                {
                    PriceName = "Позиція солярій 1 (опис 1)",
                    Price = 278,
                    PriceType = PriceItem.Solarium
                },
                new PriceEntity
                {
                    PriceName = "Позиція 4 (опис 5)",
                    Price = 178,
                    PriceType = PriceItem.Training
                },
                new PriceEntity
                {
                    PriceName = "Позиція масаж 4 (опис 4)",
                    Price = 240,
                    PriceType = PriceItem.Massage
                },
                new PriceEntity
                {
                    PriceName = "Позиція 3 (опис 3)",
                    Price = 490,
                    PriceType = PriceItem.Training
                },
                new PriceEntity
                {
                    PriceName = "Позиція солярій 1 (опис 1)",
                    Price = 400,
                    PriceType = PriceItem.Solarium
                },
                new PriceEntity
                {
                    PriceName = "Позиція солярій 3 (опис 4)",
                    Price = 680,
                    PriceType = PriceItem.Solarium
                },
                new PriceEntity
                {
                    PriceName = "Позиція 1 (опис 1)",
                    Price = 395,
                    PriceType = PriceItem.Training
                },
                new PriceEntity
                {
                    PriceName = "Позиція масаж 1 (опис 1)",
                    Price = 599,
                    PriceType = PriceItem.Massage
                },
            };

            dbContext.Prices.AddRange(prices);

            var roles = new RoleEntity[]
            {
                new RoleEntity
                {
                    RoleGuid = Guid.NewGuid(),
                    Role = "Тренер"
                },
                new RoleEntity
                {
                    RoleGuid = Guid.NewGuid(),
                    Role = "Адмін"
                }
            };

            dbContext.Roles.AddRange(roles);

            var users = new UserEntity[]
            {
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Тарас",
                    Surname = "Петренко",
                    Description = "Тренер з досвідом",
                    Category = Category.VIP
                },
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Олег",
                    Surname = "Вінник",
                    Description = "Фітнес тренер",
                    Category = Category.VIP
                },
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Ігор",
                    Surname = "Хмель",
                    Description = "Майстер спорту",
                    Category = Category.VIP
                },
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Сергій",
                    Surname = "Довженко",
                    Description = "Фітнес тренер",
                    Category = Category.First
                },
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Денис",
                    Surname = "Прокопенко",
                    Description = "Тренер з багаторічним досвідом",
                    Category = Category.First
                },
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Влад",
                    Surname = "Яма",
                    Description = "Фітнес тренер",
                    Category = Category.Second
                },
                new UserEntity
                {
                    UserGuid = Guid.NewGuid(),
                    Name = "Віктор",
                    Surname = "Сидоренко",
                    Description = "Майстер спорту з легкої атлетики",
                    Category = Category.Second
                },
            };

            dbContext.Users.AddRange(users);

            var userRoles = new UserRoleEntity[]
            {
                new UserRoleEntity
                {
                    UserGuid = users[0].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                },
                new UserRoleEntity
                {
                    UserGuid = users[1].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                },
                new UserRoleEntity
                {
                    UserGuid = users[2].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                },
                new UserRoleEntity
                {
                    UserGuid = users[3].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                },
                new UserRoleEntity
                {
                    UserGuid = users[4].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                },
                new UserRoleEntity
                {
                    UserGuid = users[5].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                },
                new UserRoleEntity
                {
                    UserGuid = users[6].UserGuid,
                    RoleGuid = roles[0].RoleGuid
                }
            };

            dbContext.AddRange(userRoles);

            dbContext.SaveChanges();
        }
    }
}
