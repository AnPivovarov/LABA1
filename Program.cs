using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LABA1
{
    class Program
    {
        public static List<Contact> contacts = new List<Contact>();


        static void Main(string[] args)
        {
            // Добавил пару конактов вручную, чтобы у нас изначально было уже что-то в списке
            contacts.Add(new Contact() { LastName = "П", Name = "Андрей", PhoneNumber = "338", Country = "Россия", Organisation = "ИТМО", JobTitle = "студент" });
            contacts.Add(new Contact() { LastName = "Петров", Name = "Василий", PatronymicName = "Михайлович", PhoneNumber = "277", Country = "Польша" });

            Console.WriteLine("Привет! Я записная книжка! Я умею:\n 1 - Просмотр всех учётных записей в краткой форме\n " +
                "2 - Просмотр всех учётных записей в полной форме\n 3 - Редактирование записи\n 4 - Удаление записи по номеру в списке\n" +
                " 5 - Удаление записи по фамилии\n 6 - Создание новой записи\n 0 - Выход");
            int action = -1;
            while (action != 0)
            {
                Console.WriteLine("\nВведите номер команды: ");
                action = Convert.ToInt32(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        ListContacts(1);
                        break;
                    case 2:
                        ListContacts(2);
                        break;
                    case 3:
                        EditContact();
                        break;
                    case 4:
                        RemoveContactById();
                        break;
                    case 5:
                        RemoveContactByLastName();
                        break;
                    case 6:
                        AddContact();
                        break;
                }
            }
        }
        private static void AddContact() // Создание нового контакта
        {
            Contact contact = new Contact();
            if (contacts.Count > 0)
            {
                contact.Id = contacts[contacts.Count - 1].Id + 1;
            }
            else contact.Id = 1;
            Console.Write("(*)Введите Фамилию: ");
            string ln = Console.ReadLine();
            while (true)
            {
                contact.LastName = ln;
                if (ln == "" || ln == null)
                {
                    Console.WriteLine("Это поле обязательно для заполнения! Введите Фамилию ещё раз");
                    ln = Console.ReadLine();
                }
                else break;
            }

            Console.Write("(*)Введите Имя: ");
            string n = Console.ReadLine();
            while (true)
            {
                contact.Name = n;
                if (n == "" || n == null)
                {
                    Console.WriteLine("Это поле обязательно для заполнения! Введите Имя ещё раз");
                    n = Console.ReadLine();
                }
                else break;
            }

            Console.Write("Введите отчество: ");
            contact.PatronymicName = Console.ReadLine();

            Console.Write("(*)Введите номер телефона: ");
            string pn = Console.ReadLine();
            while (true)
            {
                contact.PhoneNumber = pn;
                if (!pn.All(c => char.IsDigit(c)))
                {
                    Console.WriteLine("Это поле должно содержать только цифры! Введите номер телефона ещё раз");
                    pn = Console.ReadLine();
                }
                else break;
            }

            Console.Write("(*)Введите страну: ");
            string country = Console.ReadLine();
            while (true)
            {
                contact.Country = country;
                if (country == "" || country == null)
                {
                    Console.WriteLine("Это поле обязательно для заполнения! Введите Имя ещё раз");
                    country = Console.ReadLine();
                }
                else break;
            }
            Console.Write("Введите дату рождения: ");
            string d = Console.ReadLine();
            if (d != "")
            {
                contact.dateChanged = true;
                contact.BirthDate = Convert.ToDateTime(d);
            }
            Console.Write("Введите организацию: ");
            contact.Organisation = Console.ReadLine();
            Console.Write("Введите должность: ");
            contact.JobTitle = Console.ReadLine();
            Console.Write("Введите дополнительную информацию о контакте: ");
            contact.OtherInfo = Console.ReadLine();

            Console.WriteLine("Новый контакт успешно добавлен!");
            contacts.Add(contact);
        }
        private static void PrintContactShort(Contact contact) // Вывод краткой информации
        {
            Console.WriteLine(contact.Id + ". " + contact.LastName + " " + contact.Name + " " + contact.PhoneNumber);
            Console.WriteLine("==========================================");
        }
        private static void PrintContactFull(Contact contact) // Вывод полной информации
        {
            if (contact.dateChanged == false) // если мы не вводили дату рождения, чтобы не выводилось значение по-умолчанию
            {
                string str = contact.Id + ". " + contact.LastName + " " + contact.Name + " " + contact.PatronymicName + " " + contact.PhoneNumber + " "
                    + contact.Country + " " + " " + contact.Organisation + " " + contact.JobTitle + " " + contact.OtherInfo;
                Console.WriteLine(System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ")); // Нагуглил как легко удалять лишние пробелы х)

            }
            else
            {
                string str = contact.Id + ". " + contact.LastName + " " + contact.Name + " " + contact.PatronymicName + " " + contact.PhoneNumber + " "
                    + contact.Country + " " + contact.BirthDate.ToShortDateString() + " " + contact.Organisation + " " + contact.JobTitle + " " + contact.OtherInfo;
                Console.WriteLine(System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " "));
            }
            Console.WriteLine("==========================================");
        }
        private static void ListContacts(int option)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Сейчас нет ни одного контакта!");
                return;
            }
            Console.WriteLine("В текущий момент Ваш список контактов выглядит вот так:\n");
            if (option == 1)
            {
                foreach (var contact in contacts)
                {
                    PrintContactShort(contact);
                }
            }
            else if (option == 2)
            {
                foreach (var contact in contacts)
                {
                    PrintContactFull(contact);
                }
            }
        }
        private static void RemoveContactById()
        {
            ListContacts(1);
            if (contacts.Count == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("Введите номер контакта, который вы хотите удалить");
                int number = Convert.ToInt32(Console.ReadLine());
                bool contactFound = false;
                while (true)
                {
                    foreach (var item in contacts)
                    {
                        if (item.Id == number)
                        {
                            contactFound = true;
                        }
                    }
                    if (contactFound == true)
                    {
                        contacts.RemoveAt(number - 1);
                        Console.WriteLine("Контакт успешно удалён!");
                        foreach (var item in contacts)
                        {
                            if (item.Id > number) { item.Id--; } // Обновляем Id контакотов после удаления
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Введён неверный номер, повторите попытку:");
                        number = Convert.ToInt32(Console.ReadLine());
                    }
                }
            }

        }
        private static void RemoveContactByLastName()
        {
            Console.WriteLine("Введите фамилию контакта, кторого хотите удалить");
            string lastName = Console.ReadLine();
            while (true)
            {
                if (lastName == "" || lastName == null)
                {
                    Console.WriteLine("Это поле обязательно для заполнения! Введите Фамилию ещё раз");
                    lastName = Console.ReadLine();
                }
                else break;
            }
            Contact person = contacts.FirstOrDefault(x => x.LastName.ToLower() == lastName.ToLower());
            int number = 0;
            if (person == null)
            {
                Console.WriteLine("Данный контакт не найден");
                return;
            }
            else
            {
                number = person.Id;
                contacts.Remove(person);
                Console.WriteLine("Контакт успешно удалён!");
                foreach (var item in contacts)
                {
                    if (item.Id > number) { item.Id--; } // Обновляем Id контакотов после удаления
                }
            }
        }
        private static void EditContact()
        {
            ListContacts(1);
            if (contacts.Count == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("Введите номер контакта, который вы хотите отредактировать");
                int number = Convert.ToInt32(Console.ReadLine());
                while (true)
                {
                    if (number > 0 && number <= contacts.Count)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Введён неверный номер, повторите попытку:");
                        number = Convert.ToInt32(Console.ReadLine());
                    }
                }
                Contact person = contacts[number - 1];
                Console.WriteLine("\nЧто Вы хотите добавить/изменить?\n 1 - Фамилию\n 2 - Имя\n 3 - Отчество\n 4 - Номер телефона\n 5 - Страну\n " +
                        "6 - Дату рождения\n 7 - Организацию\n 8 - Должность\n 9 - дополнительную информацию о контакте\n 0 - Выход из меню редактирования");
                int action = -1;
                while (action != 0)
                {
                    Console.WriteLine("\nВведите номер команды редактирования: ");
                    action = Convert.ToInt32(Console.ReadLine());
                    switch (action)
                    {
                        case 1:
                            Console.Write("(*)Введите Фамилию: ");
                            string ln = Console.ReadLine();
                            while (true)
                            {
                                person.LastName = ln;
                                if (ln == "" || ln == null)
                                {
                                    Console.WriteLine("Это поле обязательно для заполнения! Введите Фамилию ещё раз");
                                    ln = Console.ReadLine();
                                }
                                else break;
                            }
                            break;
                        case 2:
                            Console.Write("(*)Введите Имя: ");
                            string n = Console.ReadLine();
                            while (true)
                            {
                                person.Name = n;
                                if (n == "" || n == null)
                                {
                                    Console.WriteLine("Это поле обязательно для заполнения! Введите Имя ещё раз");
                                    n = Console.ReadLine();
                                }
                                else break;
                            }
                            break;
                        case 3:
                            Console.Write("Введите отчество: ");
                            person.PatronymicName = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("(*)Введите номер телефона: ");
                            string pn = Console.ReadLine();
                            while (true)
                            {
                                person.PhoneNumber = pn;
                                if (!pn.All(c => char.IsDigit(c)))
                                {
                                    Console.WriteLine("Это поле должно содержать только цифры! Введите номер телефона ещё раз");
                                    pn = Console.ReadLine();
                                }
                                else break;
                            }
                            break;
                        case 5:
                            Console.Write("(*)Введите страну: ");
                            string country = Console.ReadLine();
                            while (true)
                            {
                                person.Country = country;
                                if (country == "" || country == null)
                                {
                                    Console.WriteLine("Это поле обязательно для заполнения! Введите Имя ещё раз");
                                    country = Console.ReadLine();
                                }
                                else break;
                            }
                            break;
                        case 6:
                            Console.Write("Введите дату рождения: ");
                            string d = Console.ReadLine();
                            if (d != "")
                            {
                                person.dateChanged = true;
                                person.BirthDate = Convert.ToDateTime(d);
                            }
                            break;
                        case 7:
                            Console.Write("Введите организацию: ");
                            person.Organisation = Console.ReadLine();
                            break;
                        case 8:
                            Console.Write("Введите должность: ");
                            person.JobTitle = Console.ReadLine();
                            break;
                        case 9:
                            Console.Write("Введите дополнительную информацию о контакте: ");
                            person.OtherInfo = Console.ReadLine();
                            break;
                    }
                }
                Console.WriteLine("Контакт успешно изменён!");
            }
        }


        public class Contact
        {
            private static int idCount = 0;
            public int Id { get; set; } = 0;
            public string LastName { get; set; }
            public string Name { get; set; }
            public string PatronymicName { get; set; }
            public string PhoneNumber { get; set; }
            public string Country { get; set; }
            public DateTime BirthDate { get; set; }
            public bool dateChanged = false;
            public string Organisation { get; set; }
            public string JobTitle { get; set; }
            public string OtherInfo { get; set; }

            public Contact()
            {
                idCount++;
                this.Id = idCount;
            }

        }
    }
}
