using System;
using System.Linq;

namespace KeyWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string key1 = @"vk, graber, grabing, manager, парсер, вк, менеджер, Грабер, Грабинг, VkManager, 
          Manager, Post, соцсеть, бесплатно, группы, групп, бесплатный, онлайн, сайты, парсеры, постов, сообщества,
          сообществ, помощник, администратора, cоздать, наполнять, источники, источник, настройки, для, каждой,
          как, указать, свою";

            var key2 = @"adipiscing dolor elit email facebook fermentum fusce google graber head location malesuada navigation sapien semper sollicitudin toggle twitter urna АДМИНИСТРАТОРА ВАШ ВК ВОЙТИ Возможность Войти ГРУППА ГРУППЫ Добавить ЗАРЕГИСТРИРОВАТЬСЯ Забыли Закрыть Запомнить ИЛИ ИСТОЧНИК Имя Использовать КАК КОНТАКТЫ Контакты ЛИЧНЫЙ НАЧАТЬ Настройки Необходимо Отправить ПОМОЩНИК Перейти Подробнее РАБОТАЕТ Регистрация Создать Сообщение ЭТО авторизации аккаунт групп группу группы для другой или источники источников кабинете каждой легко личном наполнять настройте нее несколько отправлено очень пароль пройти регистрацию свой свою способ указать".ToLower();

            for (int i = 'a'; i < 'z' + 1; i++)
            {
                key2 = key2.Replace(i.ToString(), string.Empty);
            }

            var keys = key1.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Union(key2.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)).Distinct();

            for (int i = 0; i <= keys.Count() / 10; i++)
            {
                Console.WriteLine($"{string.Join(", ", keys.Skip(i * 10).Take(10))},");
            }

            Console.ReadKey();
        }
    }
}