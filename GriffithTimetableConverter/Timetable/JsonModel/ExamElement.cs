namespace GriffithTimetableConverter.Timetable.JsonModel
{
    public struct ExamElement
    {
        public object[] AnythingArray;
        public ExamClass ExamClass;

        public static implicit operator ExamElement(object[] anythingArray) => new ExamElement() { AnythingArray = anythingArray };

        public static implicit operator ExamElement(ExamClass examClass) => new ExamElement() { ExamClass = examClass };
    }
}