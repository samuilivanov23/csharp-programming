using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr4
{
    public class Message
    {
        public Contact ContactAuthor { get; set; }
        public string ContactText { get; set; }
        public DateTime ContactCreationDate { get; set; }
        public bool ContactEdited { get; private set; }

        public Message(Contact author, string text)
        {
            ContactAuthor = author ?? throw new ArgumentNullException(nameof(author));
            ContactText = text ?? throw new ArgumentNullException(nameof(text));
            ContactCreationDate = DateTime.Now;
            ContactEdited = false;
        }

        public void ChangeMessage(string newText)
        {
            if (newText == null)
            {
                throw new ArgumentNullException(nameof(newText));
            }

            ContactText = newText;
            ContactEdited = true;
        }

        public override string ToString()
        {
            return $@"Contact created at: {ContactCreationDate}\n
                    Contact author name: {ContactAuthor.ContactName}\n
                    Contact text: {ContactText}\n
                    Is contact redacted:{ContactEdited}";
        }

        public void Deconstruct(out DateTime creationDate)
        {
            creationDate = ContactCreationDate;
        }
    }
}
