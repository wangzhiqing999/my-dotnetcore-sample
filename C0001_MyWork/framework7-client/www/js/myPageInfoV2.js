Vue.component('my-pageinfo', {

	// 声明 props
	props: {
		// 第几页.
		pageIndex: {
				  type: Number,
				  required: true
				},
		// 共几页.
		pageCount: {
				  type: Number,
				  required: true
				},

		// 基础的路径信息.
		basePath : {
				  type: String,
				  required: true
				},
		
		// 额外的路径信息.
		expPath : {
				  type: String
				}
	},

	// 计算值.
	computed: {
		// 显示分页标志.
		showPageInfo: function () {
			return this.pageCount > 1;
		},
		// 翻页数组.
		pageArray: function() {
			var resultArray = new Array();
			for(var i = 1; i <= this.pageCount; i ++) {
				resultArray.push(i);
			}
			return resultArray;
		}
	},
	
	// 内部数据.
	data: function () {
		return {
			gotoPageIndex: this.pageIndex
		};
	},

	// 模板内容.
	template: '<div class="block" v-show="showPageInfo"> \
		  <div class="pages row"> \
			<div class="col"> \
				<a v-if="pageIndex>1" v-bind:href="basePath + (pageIndex-1) + expPath" class="button button-fill button-round">上一页</a> \
				<a v-if="pageIndex==1" href="#" class="button button-fill button-round color-gray">上一页</a> \
			</div> \
			<div class="col" style="text-align:-webkit-center"> \
				<a class="item-link smart-select smart-select-init" data-open-in="sheet"> \
					<select name="gotoPage" v-model="gotoPageIndex"> \
						<option v-for="item in pageArray" v-bind:value="item"> {{item}} </option> \
					</select> \
					<div class="item-content"> \
					  <div class="item-inner" style="display:-webkit-inline-box"> \
						<div class="item-title">翻页到：</div> \
					  </div> \
					</div> \
				</a> \
			</div> \
			<div class="col"> \
				<button class="button button-fill button-round" v-on:click="gotoPage"> Go </button> \
			</div> \
			<div class="col"> \
				<a v-if="pageIndex<pageCount" v-bind:href="basePath + (pageIndex+1) + expPath" class="button button-fill button-round" >下一页</a> \
				<a v-if="pageIndex==pageCount" href="#" class="button button-fill button-round color-gray">下一页</a> \
			</div> \
		  </div> \
	  </div>',

	// 方法.
	methods: {
		gotoPage: function() {
			var queryUrl = this.basePath
					+ this.gotoPageIndex
					+ this.expPath;
			app.router.navigate(queryUrl);
		}
	}
})