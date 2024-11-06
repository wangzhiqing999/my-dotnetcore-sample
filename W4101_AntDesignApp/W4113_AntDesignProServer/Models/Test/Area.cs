namespace W4113_AntDesignProServer.Models.Test
{
    /// <summary>
    /// 用于测试 Cascader级联选择 的类.
    /// <br/>
    /// 这里是用 省市区 来模拟的.
    /// </summary>
    public class Area
    {

        public Area()
        {

        }

        public Area(string code, string name)
        {
            this.AreaCode = code;
            this.AreaName = name;
        }

        public Area(string code, string name, string parentCode)
        {
            this.AreaCode = code;
            this.AreaName = name;
            this.ParentCode = parentCode;
        }


        /// <summary>
        /// 区域代码
        /// </summary>
        public string AreaCode { set; get; }


        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { set; get; }


        /// <summary>
        /// 上级区域编号
        /// </summary>
        public string ParentCode { set; get; }



        /// <summary>
        /// 子区域.
        /// </summary>
        public List<Area> Children { set; get; }

        





        public static List<Area> GetTestAreaDataList()
        {
            List<Area> resultList = new List<Area>()
            {
                new Area("1", "北京"),

                new Area("72", "朝阳区", "1"),
                new Area("55652", "奥运村街道", "72"),
                new Area("55653", "八里庄街道", "72"),
                new Area("55655", "朝外街道", "72"),

                new Area("2800", "海淀区", "1"),
                new Area("55813", "北下关街道", "2800"),
                new Area("55816", "海淀街道", "2800"),
                new Area("55820", "清河街道", "2800"),

                new Area("2801", "西城区", "1"),
                new Area("54772", "金融街街道", "2801"),
                new Area("54776", "天桥街道", "2801"),
                new Area("54779", "月坛街道", "2801"),



                new Area("2", "上海"),

                new Area("2824","宝山区", "2"),
                new Area("51911", "罗店镇", "2824"),
                new Area("51912", "大场镇", "2824"),
                new Area("51913", "杨行镇", "2824"),


                new Area("2825","闵行区", "2"),
                new Area("51932", "莘庄镇","2825"),
                new Area("51933", "七宝镇","2825"),
                new Area("51934", "浦江镇","2825"),

                new Area("2830","浦东新区", "2"),
                new Area("51802", "高桥镇", "2830"),
                new Area("51803", "北蔡镇", "2830"),
                new Area("51810", "张江镇", "2830"),



                new Area("3", "天津"),

                new Area("51035", "东丽区", "3"),
                new Area("55901", "华明街道", "51035"),
                new Area("55902", "金桥街道", "55902"),
                new Area("55903", "金钟街道", "55903"),

                new Area("51040","红桥区", "3"),
                new Area("55874", "芥园街道", "51040"),
                new Area("55875", "铃铛阁街道", "51040"),
                new Area("55876", "三条石街道", "51040"),

                new Area("51044","滨海新区", "3"),
                new Area("55572", "新河街道", "51044"),
                new Area("55574", "新北街道", "51044"),
                new Area("55578", "寨上街道", "51044"),
            };

            return resultList;
        }




		public static List<Area> GetTestAreasTreeList()
        {
        
            List<Area> areaList = GetTestAreaDataList();


            List<Area> resultList = new List<Area>();
            // 先初始化 "省" 的节点.
            foreach (var area in areaList.Where(p => string.IsNullOrEmpty(p.ParentCode)))
            {
                resultList.Add(area);
            }


            // 遍历 "省" 的节点，为每个节点添加子节点
            foreach (var rootNode in resultList)
            {
                List<Area> childAreas = new List<Area>();

                var subAreas = areaList.Where(p => p.ParentCode == rootNode.AreaCode);

                foreach (var subArea in subAreas)
                {
					// 这里是 “省” 下面的每一个 “市”
					childAreas.Add(subArea);

					List<Area> subChildAreas = new List<Area>();

					// 遍历 “市” 下面的每一个 “县”
					var subsubAreas = areaList.Where(p => p.ParentCode == subArea.AreaCode);

					foreach (var subsubArea in subsubAreas)
					{
						// 这里是 “市” 下面的每一个 “县”						
						subChildAreas.Add(subsubArea);
					}

                    // 设置“市”的 子节点.
					subArea.Children = subChildAreas;
				}

				// 设置“省”节点的  子节点.
				rootNode.Children = childAreas;
            }
            

            return resultList;
        }



		public static IEnumerable<CascaderNode> GetTestAreaCascaderNodes()
        {

            List<CascaderNode> resultList = new List<CascaderNode>();

            List<Area> areaList = GetTestAreaDataList();

            // 先初始化 "省" 的节点.
            foreach (var area in areaList.Where(p=> string.IsNullOrEmpty(p.ParentCode)))
            {
                CascaderNode node = new CascaderNode();
                node.Value = area.AreaCode;
                node.Label = area.AreaName;                
                resultList.Add(node);
            }


            // 遍历 "省" 的节点，为每个节点添加子节点
            foreach (var rootNode in resultList)
            {
                List<CascaderNode> childNodes = new List<CascaderNode>();

                var subAreas = areaList.Where(p => p.ParentCode == rootNode.Value);

                foreach (var subArea in subAreas)
                {
                    // 这里是 “省” 下面的每一个 “市”
                    CascaderNode subNode = GetCascaderNode(subArea);

                    List<CascaderNode> subChildNodes = new List<CascaderNode>();

                    // 遍历 “市” 下面的 “县”
                    var subsubAreas = areaList.Where(p => p.ParentCode == subNode.Value);

                    foreach (var subsubArea in subsubAreas)
                    {
                        // 这里是 “市” 下面的每一个 “县”
                        CascaderNode subsubNode = GetCascaderNode(subsubArea);

                        subChildNodes.Add(subsubNode);
                    }

                    subNode.Children = subChildNodes;

                    childNodes.Add(subNode);
                }

                // 设置“省”节点的子节点.
                rootNode.Children = childNodes;
            }


            return resultList;

        }


        static CascaderNode GetCascaderNode(Area area)
        {
            return new CascaderNode()
            {
                Value = area.AreaCode,
                Label = area.AreaName,                
            };
        }


    }
}
