using BibliothequeApp.Repositories;
using BibliothequeApp.Services;
using BibliothequeApp.UI;
using System.Windows.Forms;

namespace BibliothequeApp;

class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            Console.WriteLine("Starting library management system...");
            
            // Initialize repositories
            var bookRepository = new BookRepository();
            var memberRepository = new MemberRepository();
            var loanRepository = new LoanRepository();

            // Initialize services
            var bookService = new BookService(bookRepository);
            var memberService = new MemberService(memberRepository);
            var loanService = new LoanService(loanRepository, bookRepository, memberRepository);

            // Standard Windows Forms initialization
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Create the form instance
            Console.WriteLine("Initializing main form...");
            var mainForm = new MainForm(bookService, memberService, loanService);
            
            // Show the form
            Console.WriteLine("Showing main form...");
            Application.Run(mainForm);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            Console.ReadLine();
        }
    }    
} 