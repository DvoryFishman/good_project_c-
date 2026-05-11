using System;
using System.Windows.Forms;
using DalApi;

namespace UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                Application.Run(new Menu());
            }
            catch (DalConfigException dex)
            {
                MessageBox.Show(
                    $"Failed to initialize data access:\n{dex.Message}\n\nPlease ensure the DAL assembly specified in dal-config.xml is present and referenced.",
                    "Initialization error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                // Optionally: Environment.Exit(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unexpected error:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}