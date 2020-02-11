using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PaymentGateway.Specs.Transforms
{
    [Binding]
    public class StepArgumentTransforms
    {
        [StepArgumentTransformation]
        public BankResponseDto BankResponseDtoTransform(Table table)
        {
            return table.CreateInstance<BankResponseDto>();
        }

        [StepArgumentTransformation]
        public PaymentModelDto PaymentModelDtoTransform(Table table)
        {
            return table.CreateInstance<PaymentModelDto>();
        }

        [StepArgumentTransformation]
        public IEnumerable<PaymentDto> PaymentDtosTransform(Table table)
        {
            return table.CreateSet<PaymentDto>();
        }

        [StepArgumentTransformation]
        public PaymentViewModelDto PaymentViewModelDtoTransform(Table table)
        {
            return table.CreateInstance<PaymentViewModelDto>();
        }

        [StepArgumentTransformation]
        public IEnumerable<PaymentViewModelDto> PaymentViewModelDtosTransform(Table table)
        {
            return table.CreateSet<PaymentViewModelDto>();
        }

        [StepArgumentTransformation]
        public IEnumerable<LogDto> LogDtosTransform(Table table)
        {
            return table.CreateSet<LogDto>();
        }

        [StepArgumentTransformation]
        public IEnumerable<LogViewModelDto> LogViewModelDtosTransform(Table table)
        {
            return table.CreateSet<LogViewModelDto>();
        }
    }
}
