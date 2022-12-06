using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;


namespace HelloWorld
{
    internal class Program
    {

        public class HouseData
        {
            public float Size { get; set; }
            public float Price { get; set; }
        }

        public class Prediction
        {
            [ColumnName("Score")]
            public float Price { get; set; }
        }



        static void Main(string[] args)
        {
            Step01();
            Step02();
            Step03();

            Console.WriteLine("----- Finish ! -----");
            Console.ReadLine();
        }





        /// <summary>
        /// 训练,并存储模型.
        /// </summary>
        public static void Step01()
        {

            Console.WriteLine("### 训练 ###");


            var mlContext = new MLContext();

            // 用于训练的数据.
            // 这里是例子，只有几行示例的数据，实际使用应该是比较大量的数据。
            // 这里， 实际的公式是 Price = Size +  (Size / 10)
            HouseData[] houseData = {
               new HouseData() { Size = 1.1F, Price = 1.2F },
               new HouseData() { Size = 1.9F, Price = 2.1F },
               new HouseData() { Size = 2.8F, Price = 3.1F },
               new HouseData() { Size = 3.4F, Price = 3.7F },
               new HouseData() { Size = 4.8F, Price = 5.3F },
               new HouseData() { Size = 5.4F, Price = 5.9F } };


            // 将训练数据收集并加载到 IDataView 对象中.
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(houseData);


            // 将一个或多个输入列连接到新的输出列中。
            // 定义 ["Size"] 为输入。 "Features" 为输出.
            ColumnConcatenatingEstimator cce = mlContext.Transforms.Concatenate("Features", new[] { "Size" });

            // 训练的选项.
            var options = new SdcaRegressionTrainer.Options
            {
                LabelColumnName = nameof(HouseData.Price),

                // Increase the maximum number of passes over training data. Similar
                // to ConvergenceTolerance, this value specifics the hard iteration
                // limit on the training algorithm.
                MaximumNumberOfIterations = 100,
            };

            // 使用线性回归模型预测目标。
            SdcaRegressionTrainer trainer = mlContext.Regression.Trainers.Sdca(options);

            // 指定操作的管道，以提取特征并应用机器学习算法.
            var pipeline = cce.Append(trainer);


            // 通过在管道上调用 Fit() 来训练模型.
            var model = pipeline.Fit(trainingData);


            // 存储训练的结果.
            mlContext.Model.Save(model, trainingData.Schema, "HelloWorld.zip");
        }





        /// <summary>
        /// 模型评估
        /// 训练模型后，如何了解其进行未来预测的表现如何？ 
        /// 借助 ML.NET，可以根据一些新的测试数据评估模型。
        /// </summary>
        public static void Step02()
        {

            Console.WriteLine("### 评估 ###");

            var mlContext = new MLContext();

            // 加载模型.
            ITransformer model = mlContext.Model.Load("HelloWorld.zip", out var _);

            // 这里预期放的， 是用于验证的数据.
            // 也就是前面的 训练出来的模型，现在要 验证一下，看看可靠不可靠.
            HouseData[] testHouseData = {
               new HouseData() { Size = 2.1F, Price = 2.3F },
               new HouseData() { Size = 2.9F, Price = 3.1F },
               new HouseData() { Size = 3.8F, Price = 4.1F },
               new HouseData() { Size = 4.4F, Price = 4.7F },
               new HouseData() { Size = 5.8F, Price = 6.3F },
               new HouseData() { Size = 6.4F, Price = 6.9F } };


            // 注意：
            // 这里是例子，所以手动造一堆数据，来验证。
            // https://learn.microsoft.com/zh-cn/dotnet/machine-learning/how-to-guides/train-machine-learning-model-ml-net
            // 实际运行中，比较理想的处理机制，是 使用 TrainTestSplit 方法将数据拆分为训练集和测试集。
            // 结果将是一个 TrainTestData 对象，其中包含两个 IDataView 成员，一个用于训练集，另一个用于测试集。

            IDataView testHouseDataView = mlContext.Data.LoadFromEnumerable(testHouseData);





            var testPriceDataView = model.Transform(testHouseDataView);

            var metrics = mlContext.Regression.Evaluate(testPriceDataView, labelColumnName: "Price");

            Console.WriteLine($"R^2: {metrics.RSquared:0.##}");
            Console.WriteLine($"RMS error: {metrics.RootMeanSquaredError:0.##}");


        }




        /// <summary>
        /// 读取模型，来使用。
        /// </summary>
        public static void Step03()
        {

            Console.WriteLine("### 使用 ###");

            var mlContext = new MLContext();


            // 将模型加载回 ITransformer 对象.
            ITransformer mlModel = mlContext.Model.Load("HelloWorld.zip", out var _);


            // 通过调用 CreatePredictionEngine.Predict() 进行预测.
            PredictionEngine<HouseData, Prediction> engine = mlContext.Model.CreatePredictionEngine<HouseData, Prediction>(mlModel);

            var size = new HouseData() { Size = 5F };
            var price = engine.Predict(size);

            Console.WriteLine($"Size = {size.Size} ;  Price = {price.Price}");


        }


    }
}