using System;
using System.Reflection;

namespace SchneiderElectric.InvoicingSystem.Presentation.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}