using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MatrixMultiplicationBenchmark
{
    public partial class MainForm : Form
    {
        private List<List<double>> allExperimentsTimes = new List<List<double>>();
        private List<List<string>> allExperimentsSizes = new List<List<string>>();
        private Chart chart;
        private Button startButton;
        private Button stopButton;
        private Button nextButton;
        private int experimentCount = 0;
        private const int TotalExperiments = 3;
        private const int ExperimentsPerSize = 1;

        private bool isExperimentRunning = false;
        private Stopwatch experimentStopwatch = new Stopwatch();
        private Random random = new Random();

        public MainForm()
        {
            this.Text = "Matrix Multiplication Benchmark (n ≠ m)";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            startButton = new Button();
            startButton.Text = $"Start Experiment {experimentCount + 1}/{TotalExperiments}";
            startButton.Size = new Size(200, 40);
            startButton.Location = new Point(20, 720);
            startButton.Click += (sender, e) => RunExperiment();
            this.Controls.Add(startButton);

            stopButton = new Button();
            stopButton.Text = "Stop Experiment";
            stopButton.Size = new Size(200, 40);
            stopButton.Location = new Point(230, 720);
            stopButton.Click += (sender, e) => StopExperiment();
            stopButton.Enabled = false;
            this.Controls.Add(stopButton);

            nextButton = new Button();
            nextButton.Text = "Next Experiment";
            nextButton.Size = new Size(200, 40);
            nextButton.Location = new Point(440, 720);
            nextButton.Click += (sender, e) => PrepareNextExperiment();
            nextButton.Enabled = false;
            this.Controls.Add(nextButton);

            SetupChart();
        }

        private void SetupChart()
        {
            chart = new Chart();
            chart.Location = new Point(20, 20);
            chart.Size = new Size(1150, 680);
            this.Controls.Add(chart);

            chart.ChartAreas.Add(new ChartArea("MainArea"));
            chart.Titles.Add("Matrix Multiplication Performance (n ≠ m)");
            chart.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            chart.ChartAreas[0].AxisX.Title = "Point Sequence";
            chart.ChartAreas[0].AxisY.Title = "Execution Time (seconds)";

            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisY.Minimum = 0;

            chart.Legends.Add(new Legend());
            chart.Legends[0].Docking = Docking.Bottom;

            // Добавляем начальную серию чтобы график был виден
            var initialSeries = new Series("Waiting for data...")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.LightGray,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 2
            };
            initialSeries.Points.AddXY(0, 0);
            chart.Series.Add(initialSeries);
        }

        private async void RunExperiment()
        {
            if (isExperimentRunning) return;

            isExperimentRunning = true;
            startButton.Enabled = false;
            stopButton.Enabled = true;
            nextButton.Enabled = false;

            List<double> executionTimes = new List<double>();
            List<string> matrixSizes = new List<string>();

            experimentStopwatch.Restart();

            Console.WriteLine($"=== Starting Experiment {experimentCount + 1}/{TotalExperiments} ===");
            Console.WriteLine("Matrix multiplication: A(n×m) × B(m×p) where n ≠ m ≠ p");

            int pointCounter = 0;
            int baseSize = 50;

            // Очищаем временные данные перед началом
            executionTimes.Clear();
            matrixSizes.Clear();

            while (isExperimentRunning)
            {
                pointCounter++;

                int n = baseSize + (pointCounter * 5);
                int m = n + random.Next(15, 36);
                int p = m + random.Next(-25, 26);

                while (Math.Abs(m - n) < 10) m = n + random.Next(15, 36);
                while (Math.Abs(p - m) < 10) p = m + random.Next(-25, 26);

                string sizeStr = $"A({n}×{m}) × B({m}×{p})";
                matrixSizes.Add(sizeStr);

                Console.WriteLine($"Point {pointCounter}: {sizeStr}");

                double[,] matrixA = GenerateRandomMatrix(n, m, random);
                double[,] matrixB = GenerateRandomMatrix(m, p, random);

                var pointStopwatch = Stopwatch.StartNew();
                double[,] result = MultiplyMatrices(matrixA, matrixB);
                pointStopwatch.Stop();

                double elapsedSeconds = pointStopwatch.Elapsed.TotalSeconds;
                executionTimes.Add(elapsedSeconds);

                Console.WriteLine($"  Time: {elapsedSeconds:F3}s");

                // ОБНОВЛЯЕМ ГРАФИК ПОСЛЕ КАЖДОЙ ТОЧКИ
                UpdateChart();
                Application.DoEvents();

                if (isExperimentRunning)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                }
            }

            experimentStopwatch.Stop();

            if (executionTimes.Count > 0)
            {
                allExperimentsTimes.Add(new List<double>(executionTimes));
                allExperimentsSizes.Add(new List<string>(matrixSizes));
                experimentCount++;

                Console.WriteLine($"\n🎉 Experiment {experimentCount} completed");
                Console.WriteLine($"Points: {executionTimes.Count}, Time: {experimentStopwatch.Elapsed.TotalSeconds:F0}s");

                // ПРИНУДИТЕЛЬНОЕ ОБНОВЛЕНИЕ ПОСЛЕ ОСТАНОВКИ
                UpdateChart();
                Application.DoEvents();
            }

            isExperimentRunning = false;
            stopButton.Enabled = false;

            if (experimentCount < TotalExperiments)
            {
                nextButton.Enabled = true;
                MessageBox.Show($"Experiment {experimentCount} completed!\n" +
                               $"Time: {experimentStopwatch.Elapsed.TotalSeconds:F0}s\n" +
                               $"Points: {executionTimes.Count}",
                              "Experiment Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                startButton.Text = "All Experiments Completed";
                ShowFinalResults();
            }
        }

        private void StopExperiment()
        {
            if (isExperimentRunning)
            {
                isExperimentRunning = false;
                Console.WriteLine("\n⏹️ Experiment stopped by user");
                stopButton.Enabled = false;

                // НЕМЕДЛЕННОЕ ОБНОВЛЕНИЕ ГРАФИКА ПОСЛЕ ОСТАНОВКИ
                UpdateChart();
                Application.DoEvents();
            }
        }

        private void PrepareNextExperiment()
        {
            if (experimentCount < TotalExperiments)
            {
                startButton.Text = $"Start Experiment {experimentCount + 1}/{TotalExperiments}";
                startButton.Enabled = true;
                nextButton.Enabled = false;

                Console.WriteLine($"\n📋 Ready for Experiment {experimentCount + 1}/{TotalExperiments}");
            }
        }

        private double[,] GenerateRandomMatrix(int rows, int cols, Random random)
        {
            double[,] matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = Math.Round(random.NextDouble() * 100, 2);
                }
            }
            return matrix;
        }

        private double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int colsB = matrixB.GetLength(1);

            double[,] result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = Math.Round(sum, 4);
                }
            }

            return result;
        }

        private void UpdateChart()
        {
            // Очищаем только если есть данные
            if (allExperimentsTimes.Count > 0)
            {
                chart.Series.Clear();
            }

            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple };
            string[] seriesNames = { "Experiment 1", "Experiment 2", "Experiment 3", "Experiment 4", "Experiment 5" };

            for (int exp = 0; exp < allExperimentsTimes.Count; exp++)
            {
                if (allExperimentsTimes[exp].Count > 0) // Проверяем что есть данные
                {
                    var series = new Series(seriesNames[exp])
                    {
                        ChartType = SeriesChartType.Line,
                        Color = colors[exp],
                        BorderWidth = 2,
                        MarkerStyle = MarkerStyle.Circle,
                        MarkerSize = 6,
                        MarkerColor = colors[exp]
                    };

                    for (int i = 0; i < allExperimentsTimes[exp].Count; i++)
                    {
                        int pointIndex = series.Points.AddXY(i + 1, allExperimentsTimes[exp][i]);

                        if (i % 10 == 0 || i == allExperimentsTimes[exp].Count - 1)
                        {
                            series.Points[pointIndex].Label = $"{allExperimentsTimes[exp][i]:F1}s";
                        }
                        series.Points[pointIndex].LabelForeColor = colors[exp];
                        series.Points[pointIndex].ToolTip = $"Point {i + 1}: {allExperimentsSizes[exp][i]}\nTime: {allExperimentsTimes[exp][i]:F3}s";
                    }

                    chart.Series.Add(series);
                }
            }

            // Если нет данных, показываем заглушку
            if (chart.Series.Count == 0)
            {
                var emptySeries = new Series("No data yet")
                {
                    ChartType = SeriesChartType.Point,
                    Color = Color.LightGray,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 2
                };
                emptySeries.Points.AddXY(0, 0);
                chart.Series.Add(emptySeries);
            }

            chart.ChartAreas[0].RecalculateAxesScale();
            chart.Invalidate(); // Принудительная перерисовка
        }

        private void ShowFinalResults()
        {
            string results = "All Experiments Completed!\n\n";
            double totalAllTime = 0;

            for (int exp = 0; exp < allExperimentsTimes.Count; exp++)
            {
                double totalTime = allExperimentsTimes[exp].Sum();
                totalAllTime += totalTime;

                results += $"Experiment {exp + 1}:\n";
                results += $"  Points: {allExperimentsTimes[exp].Count}\n";
                results += $"  Total time: {totalTime:F1}s\n";
                results += $"  Average: {allExperimentsTimes[exp].Average():F3}s\n\n";
            }

            results += $"Grand Total: {totalAllTime:F1} seconds\n";
            results += $"Total points: {allExperimentsTimes.Sum(exp => exp.Count)}";

            MessageBox.Show(results, "All Experiments Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
