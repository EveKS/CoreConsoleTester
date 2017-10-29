using System;
using System.Text.RegularExpressions;

namespace rexegReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = @"1. Морковь и шампиньоны нарезать тонкими ломтиками, лук – кольцами, чеснок мелко нарубить.
2. В большой толстостенной кастрюле нагреть 50 мл воды и оливковое масло, добавить порезанные луковицу, морковь, чеснок и шампиньоны. Тушить в течение 5–7 минут на среднем огне.
3. Добавить капусту, нарезанный кабачок и остальные ингредиенты кроме соли и перца.
4. Накрыть крышкой и варить 2 часа на очень слабом огне. В конце добавить соль и перец.
5. Рекомендую дать настояться перед употреблением сутки.

Автор рецепта - Владислав Носик

Приятного аппетита!";

            string pattern = @"(Автор)\s?([^:^-]*[:-])([^\n]*[\n]?)";
            string replacePattern = string.Empty;

            var regex = new Regex(pattern, RegexOptions.Compiled);
            text = regex.Replace(text, replacePattern);

            Console.WriteLine(text);

            Console.Read();
        }
    }
}