using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Text.RegularExpressions;
using System.Text;

namespace MVC
{
    public static class Translate
    {
        public static string It(string key)
        {
            if (dictionary.Keys.Contains(key))
                return dictionary[key];
            else 
                return key;
        }
        private static Dictionary <string, string> dictionary = new Dictionary<string, string>()
        {
            { "Web portal of law help | Protect your rights", "Портал юридичної допомоги | Захисти свої права"},
            { "Web portal of law help | Result", "Портал юридичної допомоги | Результат "},
            // MAIN MENU
            { "Main", "Головна" },
            { "Admin menu", "Адмін меню" },
            // ADMIN MENU VIEW
            { "Administrative menu", "Адміністративне меню" },
            { "General list", "Загальний список" },
            { "The overall structure of lawsuits that are within the categories and subcategories", "Загальна структура позовів, що розміщені в межах категорій та підкатегорій" },
            { "Categories of lawsuits", "Категорії позовів" },
            { "Each category has subcategories, which are located lawsuits", "Кожна категорія має підкатегоріїї, в яких розміщуються позови" },
            { "Subategories of lawsuits", "Підкатегорії позовів" },
            { "Each subcategory contains one or more lawsuit", "Кожна підкатегорія містить один, або кілька позовів" },
            { "Lawsuits", "Позови" }, 
            { "Creating and editing lawsuits with pre-created blocks", "Створення та редагування позовів з попередньо створених блоків" },  
            { "Groups of blocks", "Групи блоків" },  
            { "Blocks can be created only in advance unspecified groups", "Блоки можуть бути створені лише у наперед визначених групах" },
            { "Blocks", "Блоки" },
            { "Lawsuit consist of blocks containing one or more parts of the text","Позови складаються з блоків, що містять одну чи кілька текстових частин" }, 
            { "Text styles", "Стилі тексту" },
            { "Each text part should have a text style", "Кожна текстова частина має мати певний текстовий стиль" }, 
            { "Fields", "Поля" },   
            { "Text parts of the blocks may have markers that are filled with the user through the fields", "Текстові частини блоків можуть мати маркери, що заповнюються користувачем за допомогою полів" },  
            { "Web resources", "Веб ресурси" }, 
            { "Text Web resources of site", "Текстові веб ресурси порталу" },
            // EDITORS (ADD, EDIT, REMOVE)
            { "Name", "Назва" },
            { "Text", "Текст" },
            { "Style", "Cтиль" },
            { "Weight in list", "Вага у списку" },
            { "Add new", "Додати новий" },
            { "Back to list", "Вернутись до списку" },
            { "Add", "Додати" },
            { "Change", "Змінити" },
            { "Remove", "Видалити" },
            { "Preview", "Перегляд" },
            { "Submit", "Зберегти" },
            { "None", "Немає" },
            { "Back", "Назад" },
            { "List is empty", "Список пустий" },
            { "To list", "До списку" },
            { "Continue", "Продовжити" },
            // CATEGORY CONTROLLER
            { "Category", "Категорія" },
            { "New category", "Нова категорія" },
            { "Change category", "Зміна категорії" },
            // SUBCATEGORY CONTROLLER
            { "Subcategory", "Підкатегорія" },
            { "Subcategories", "Підкатегорії" },
            { "New subcategory", "Нова підкатегорія" },
            { "Change subcategory", "Зміна підкатегорії" },
            // LAWSUITS CONTROLLER
            { "Lawsuit", "Позов" },
            { "Add, delete blocks...", "Додавання, видалення блоків..." },
            { "Change lawsuit properties", "Зміна властивостей позову" },
            { "New lawsuit", "Новий позов" },
            // LAWSUITBLOCK CONTROLLER
            { "Existing blocks", "Існуючі блоки" },
            { "Added blocks", "Додані блоки" },
            { "Text preview", "Перегляд тексту" },
            { "Fields preview", "Перегляд полів" },          
            // GROUP CONTROLLER
            { "Group", "Група" },
            { "New group", "Нова група" },
            { "Change group", "Зміна групи" },
            // BLOCK CONTROLLER
            { "Block", "Блок" },
            { "New block", "Новий блок" },
            { "Change block", "Зміна блоку" },
            { "Field(-s) include type", "Тип підключення поля(-ів)" },
            { "Block content type", "Тип контенту блоку" },
            { "Block has no text parts", "У блоці відсутні текстові частини" },
            { "Submit and add text part", "Зберегти та додати текстову частину" },
            // TEXT STYLE CONTROLLER
            { "Text style", "Стиль тексту" },
            { "New text style", "Новий стиль тексту" },
            { "Change text style", "Зміна стилю тексту" },
            { "Margin type", "Тип відступу" },
            { "Align type", "Тип вирівнювання" },
            { "Font style", "Стиль шрифту" },
            { "Font size", "Розмір шрифта" },
            { "Bold font", "Жирний шрифт" },
            { "Underlined font", "Підкреслений шрифт" },
            { "Italic font", "Нахилений шрифт" },
            // PART CONTROLLER
            { "Text parts", "Текстові частини" },
            { "New text part", "Нова текстова частина" },
            { "Change text part", "Зміна текстової частини" },
            { "Markers", "Маркери" },
            { "Warning: The text can only be used predefined tokens can add them when creating a new field", "Увага!В тексті можна застосовувати лише попередньо визначені маркери, додати їх можна при створенні нового поля" },        
            // FIELD CONTROLLER
            { "Field", "Поле" },
            { "New field", "Нове поле" },
            { "Change field", "Зміна поля" },
            { "Marker", "Маркер" },
            { " (only letters and numbers without spaces)", " (лише літери та цифри без пробілів)" },
            { "Content type", "Тип контенту" },
            { "Data for lawsuit", "Дані для позову" },
            { "Relevance and validity of the claim depends on the data entered", "Належність та обгрунтованість позову залежить від введених даних" },    
            // WEB RESOURCE CONTROLLER
            { "Web resource", "Веб ресурс" },
            { "HomePage", "Стартова сторінка" },
            { "Advices", "Поради" },
            { "СourtFee", "Cудовий збір" },
            { "Title", "Заголовок"},
            { "Body", "Основний текст"},
            { "Site web resources", "Веб ресурси сайту" },
            { "Lawsuit features", "Особливості позову" },
            // ERROR PAGE
            { "Database error", "Помилка бази даних"},
            { "Error", "Помилка" },
            { "Request can't be resolved because of error", "Запит не може бути задоволений через помилку"},
            // DB TRY SAVE CHANGES MESSAGES
            { "Data can't be added", "Дані не можуть бути додані" },
            { "Data can't be changed", "Дані не можуть бути змінені" },
            { "Data can't be removed", "Дані не можуть бути видалені" },
            // VALIDATION ERRORS
            { "Value can't be empty", "Значення не може бути пустим" },
            { "Marker not defined", "Маркер не визначено" },
            { "Marker not in format", "Маркер не відповідає формату" },
            { "Value can't be null or negative", "Значення не може бути нульовим або від`ємним" },
            { "Date is not valid", "Значення дати не вірне" },
            // DEFINES BLOCK INCLUDE TYPES
            { "Automatic", "Автоматично" },
            { "By choice", "За вибором" },
            // BLOCK CONTENT TYPES
            { "Initial details", "Початкові реквізити" },
            { "Introduction part", "Вступна частина" },
            { "Reasoning part", "Мотивувальна частина" },
            { "Requesting part", "Прохальна частина" },
            { "Final details", "Кінцеві реквізити" },
            // FIELD DATA TYPES
            { "text", "текст" },
            { "number", "число" },
            { "date", "дата" },
            { "money", "гроші" },
            { "question", "запитання" },
            // TEXT STYLE TEXT ALIGN
            { "left", "ліве" },
            { "center", "по центру" },
            { "right", "праве" },
            { "justify", "розтягнуте" },
            // TEXT STYLE MARGIN LEFT
            { "none", "відсутній" },
            { "less", "меншe" },
            { "heading", "заголовок" },
            { "more", "більше" },
            // TEXT STYLE TAGS
            { "Paragraph <p>" , "Параграф <p>" },
            { "The largest <h1>" , "Найбільший <h1>" },
            { "Large <h2>" , "Великий <h2>" },
            { "Larger <h3>" , "Більший <h3>" },
            { "Normal <h4>" , "Нормальний <h4>" },
            { "Less <h5>" , "Менший <h5>" },
            // ACCOUNT CONTROLLER
            { "Log in", "Вхід" },
            { "Exit" , "Вихід" },
            { "Login" , "Логін" },
            { "Password" , "Пароль" },
        };
    }
}