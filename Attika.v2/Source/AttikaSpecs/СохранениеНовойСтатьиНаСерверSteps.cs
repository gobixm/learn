using TechTalk.SpecFlow;

namespace Infotecs.Attika.AttikaSpecs
{
    [Binding]
    public class СохранениеНовойСтатьиНаСерверSteps
    {
        [Given(@"я запускаю программу AttikaGui")]
        public void ДопустимЯЗапускаюПрограммуAttikaGui()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"и вижу заготовку под новую статью")]
        public void ДопустимИВижуЗаготовкуПодНовуюСтатью()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"я ввожу Заголовок")]
        public void ЕслиЯВвожуЗаголовок()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"ввожу Текст")]
        public void ЕслиВвожуТекст()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"нажимаю кнопку Сохранить")]
        public void ЕслиНажимаюКнопкуСохранить()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"должно отобразиться ""(.*)"" в статусе")]
        public void ТоДолжноОтобразитьсяВСтатусе(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"на панели навигации добавиться новая статья")]
        public void ТоНаПанелиНавигацииДобавитьсяНоваяСтатья()
        {
            ScenarioContext.Current.Pending();
        }
    }
}