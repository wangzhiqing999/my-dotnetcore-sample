
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
				}
	},

	// 计算值.
	computed: {
		// 显示分页标志.
		showPageInfo: function () {
			return this.pageCount > 1;
		}
	},

	// 模板内容.
	template: '<div class="block"> \
		  <div class="col"> {{pageIndex}} / {{pageCount}} </div> \
		  <div class="pages row" v-show="showPageInfo"> \
			<div class="col"> \
				<button class="button button-fill button-round" @click="pageUp()" :disabled="pageIndex==1">上一页</button> \
			</div> \
			<div class="col"> \
				<button class="button button-fill button-round" @click="pageDown()" :disabled="pageIndex==pageCount">下一页</button> \
			</div> \
		  </div> \
	  </div>',

	// 方法.
	methods: {
		pageUp:function() {
			// 上一页.
			this.$emit('page-up');
		},
		pageDown:function() {
			// 下一页.
			this.$emit('page-down');
		}
	}
})