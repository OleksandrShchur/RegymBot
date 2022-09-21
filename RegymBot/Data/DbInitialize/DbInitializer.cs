using Microsoft.EntityFrameworkCore;
using PasswordHashing;
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

            // dbContext.UserRoles.RemoveRange(dbContext.UserRoles);
            // dbContext.UserClubs.RemoveRange(dbContext.UserClubs);
            // dbContext.Credentials.RemoveRange(dbContext.Credentials);
            // dbContext.TGUsers.RemoveRange(dbContext.TGUsers);
            // dbContext.Feedbacks.RemoveRange(dbContext.Feedbacks);
            // dbContext.Prices.RemoveRange(dbContext.Prices);
            // dbContext.Clubs.RemoveRange(dbContext.Clubs);
            // dbContext.StaticMessages.RemoveRange(dbContext.StaticMessages);
            // dbContext.Users.RemoveRange(dbContext.Users);
            // dbContext.Roles.RemoveRange(dbContext.Roles);
            // dbContext.Pages.RemoveRange(dbContext.Pages);
            // dbContext.Clients.RemoveRange(dbContext.Clients);
            // dbContext.AdminsInfo.RemoveRange(dbContext.AdminsInfo);
            // dbContext.AdminsRegistrationLinks.RemoveRange(dbContext.AdminsRegistrationLinks);

            if (dbContext.StaticMessages.Any())
            {
                return; // DB has been seeded
            }

            var credentials = new CredentialsEntitiy[]
            {
                new CredentialsEntitiy
                {
                    Login = "admin",
                    Password = PasswordHasher.Hash("superpassword"),
                },
            };

            dbContext.Credentials.AddRange(credentials);

            var clubs = new ClubEntity[]
            {
                new ClubEntity
                {
                    ClubId = (int)RegymClub.None,
                    Name = RegymClub.None.ToString(),
                },
                new ClubEntity
                {
                    ClubId = (int)RegymClub.Apollo,
                    Name = RegymClub.Apollo.ToString(),
                },
                new ClubEntity
                {
                    ClubId = (int)RegymClub.PSHKN,
                    Name = RegymClub.PSHKN.ToString(),
                },
                new ClubEntity
                {
                    ClubId = (int)RegymClub.Vavylon,
                    Name = RegymClub.Vavylon.ToString(),
                },
            };
            dbContext.Clubs.AddRange(clubs);

            var adminsRegistrationLinks = new AdminsRegistrationLinks
            {
                Apollo = "https://telegram.me/regym_club_bot?start=apollo_admin",
                Pshkn = "https://telegram.me/regym_club_bot?start=pshkn_admin",
                Vavylon = "https://telegram.me/regym_club_bot?start=vavylon_admin",
            };

            dbContext.AdminsRegistrationLinks.Add(adminsRegistrationLinks);

            var adminsInfo = new AdminsInfo
            {
                AdminApolloLogin = "test",
                AdminPSHKNLogin = "test",
                AdminVavylonLogin = "test",
            };

            dbContext.AdminsInfo.Add(adminsInfo);

            var staticMessages = new StaticMessageEntity[]
            {
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Start,
                    Message = @"Вітання!  Я чат-бот мережі спорт-хабів ReGym.

 Заняття спортом мають бути доступними та легкими.  Тому я намагатимусь допомогти тобі вирішити всі завдання прямо в Telegram!

 Що я можу:

 •знайти наш найближчий до вас 🏋️спортзал;
 •розповісти ℹ️ про послуги хаба;
 •допомогти записатися на 💪тренування;
 •вибрати 🥋 тренера;
 •допомогти зв'язатися з адміністратором 🧜‍♀️."
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Massage,
                    Message = "В наших залах в PSHKN центр и ТГ Вавилон ви можете відвідати массажні кабінети, в яких вам допоможуть розслабитись після тренування за допомогою пятнадцати видів массажу."
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Price,
                    Message = "Прайс лист тренувань:"
                },
                 new StaticMessageEntity
                {
                    PageId = (int)BotPage.Social,
                    Message = @"🌸 [Instagram](https://www.instagram.com/regym.hub)
👤 [Facebook](https://www.facebook.com/regym.hub)"
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.LeaveFeedback,
                    Message = "Розкажіть, що вам сподобалось та як нам стати краще для вас?"
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.SelectClub,
                    Message = "Виберіть клуб:"
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Solarium,
                    Message = "В наших залах в ТГ Вавилон ТРК Апполо ви можете відвідати солярій."
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Club_Apollo,
                    Message = "Телефон Аполло\n+380999999999\n\"Адреса:\nвул. Хмельницького 68\nРозклад:\nПН - СБ: 08 - 21\nВС: 9 - 18\""
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Club_Vavylon,
                    Message = "Телефон Вавилон\n+380999999999\n\"Адреса:\nвул. Хмельницького 68\nРозклад:\nПН - СБ: 08 - 21\nВС: 9 - 18\""
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Club_Pshkn,
                    Message = "Телефон PSHKN\n+380999999999\n\"Адреса:\nвул. Хмельницького 68\nРозклад:\nПН - СБ: 08 - 21\nВС: 9 - 18\""
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.Category,
                    Message = "Виберіть категорію:"
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.TrainingSchedule,
                    Message = "Напишіть нам, на яке тренування ви хочете записатися?"
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.GetUserName,
                    Message = "Вкажіть своє ім'я та прізвище."
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.GetUserPhone,
                    Message = "Напишіть мобільний номер, по якому ми зможемо з вами сконтактуватись."
                },
                new StaticMessageEntity
                {
                    PageId = (int)BotPage.FinishEnrolInGroup,
                    Message = "Ваш запис збережено! Найближчим часом ми із вами зв'яжемось. Дякуємо, що обрали саме нас!"
                }
            };

            dbContext.StaticMessages.AddRange(staticMessages);

            var pages = new PageEntity[]
            {
                new PageEntity
                {
                    PageId = (int)BotPage.Start,
                    Name = "Стартова сторінка"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Massage,
                    Name = "Сторінка <Масаж>"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Price,
                    Name = "Сторінка цін"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Social,
                    Name = "Соціальні Мережи"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.LeaveFeedback,
                    Name = "Сторінка <Залишити відгук>"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.SelectClub,
                    Name = "Сторінка вибору клубу"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Solarium,
                    Name = "Сторінка <Солярій>"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Club_Apollo,
                    Name = "Сторінка <Клуб Аполло>"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Club_Vavylon,
                    Name = "Сторінка <Клуб Вавилон>"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Club_Pshkn,
                    Name = "Сторінка <Клуб PSHKN>"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.Category,
                    Name = "Сторінка вибору категорій"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.CoachList,
                    Name = "Сторінка вибору тренерів"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.TrainingSchedule,
                    Name = "Сторінка запису в групу"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.GetUserName,
                    Name = "Сторінка заповнення імені клієнта"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.GetUserPhone,
                    Name = "Сторінка заповнення мобільного клієнта"
                },
                new PageEntity
                {
                    PageId = (int)BotPage.FinishEnrolInGroup,
                    Name = "Завершення запису в групу"
                }
            };

            dbContext.Pages.AddRange(pages);

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
                    UserGuid = new Guid("970F47F4-21FD-4B62-BD9B-E0151ACD12FF"),
                    Name = "Олександр",
                    Surname = "Бабак",
                    Description = "Тренер спорт хаба з єдиноборств. Кікбоксинг К1, тайський бокс – в цьому він профі.",
                    Category = Category.VIP
                },
                new UserEntity
                {
                    UserGuid = new Guid("20E774C9-D9DB-4AEF-BFC5-6578624AF075"),
                    Name = "Ника",
                    Surname = "Бондарь",
                    Description = "«Стань лучшей версией себя!» — главный девиз Ники Бондарь в тренажерном зале. Каждую свою клиентку она буквально берет за руку и ведет по всем этапам «преображения».",
                    Category = Category.First
                },
                new UserEntity
                {
                    UserGuid = new Guid("6CC5C859-84D1-4C06-A2C8-B82ADC9C2317"),
                    Name = "Антон",
                    Surname = "Білодід",
                    Description = "Антон Білодід вже 8 років «в спорті». Це означає – власний спортивний досвід і ведення тренувань. Тренажерний зал, Кроссфіт, функціональний тренінг – кожен з напрямків він освоїв досконало.",
                    Category = Category.First
                },
                new UserEntity
                {
                    UserGuid = new Guid("2AB8C614-D51F-4C25-AFE3-7AACA485A9EC"),
                    Name = "Ростислав",
                    Surname = "Трухін",
                    Description = "Ростислав Трухін – супер тренер, і це не перебільшення. 😎",
                    Category = Category.VIP
                },
                new UserEntity
                {
                    UserGuid = new Guid("C8716555-3030-4D70-881F-46352CD8B543"),
                    Name = "Олексій",
                    Surname = "Янок",
                    Description = "Майстер спорту з гирьового спорту.",
                    Category = Category.Second
                },
                new UserEntity
                {
                    UserGuid = new Guid("44DD835C-8F4F-4E29-80D1-206BCFB7BC76"),
                    Name = "Богдан",
                    Surname = "Волков",
                    Description = "Майстер спорту міжнародного класу з жиму лежачи без екіпіровки WRP Federation.",
                    Category = Category.Second
                },
            };

            dbContext.Users.AddRange(users);

            var userClubs = new UserClubEntity[]
            {
                new UserClubEntity
                {
                    UserRef = users[0].UserGuid,
                    ClubRef = clubs[1].ClubId
                },
                new UserClubEntity
                {
                    UserRef = users[0].UserGuid,
                    ClubRef = clubs[2].ClubId
                },
                new UserClubEntity
                {
                    UserRef = users[1].UserGuid,
                    ClubRef = clubs[1].ClubId
                },
                new UserClubEntity
                {
                    UserRef = users[1].UserGuid,
                    ClubRef = clubs[3].ClubId
                },
            };

            dbContext.UserClubs.AddRange(userClubs);

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
            };

            dbContext.AddRange(userRoles);

            dbContext.SaveChanges();
        }
    }
}
