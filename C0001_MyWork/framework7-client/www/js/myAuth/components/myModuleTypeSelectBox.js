"use strict";

Vue.component('my-moduletype-select-box', {

	// 声明 props
	props: {
		// 选择的数据.
		selected : {
				  type: String,
				  required: false
				}
	},

	// 数据.
	data: function () {
		return {
			selectedItem: this.selected,
			dataList: []
		}
	},

	// 事件.
	beforeCreate: function () {
		var _this = this;
		var requestData = {
			pageNo : 1,
			pageSize : 100,
		};
		_myServiceList.myModuleType.list(requestData, function(data) {
			_this.dataListChange(data.queryResultData);
		});
	},

	// 模板内容.
	template: '<select name="moduletype" v-model="selectedItem" v-on:change="selectedChange"> \
		<option v-for="option in dataList" v-bind:value="option.moduleTypeCode" > \
			{{ option.moduleTypeName }} \
		</option> \
	</select>',

	// 方法.
	methods: {
		// 数据变化.
		dataListChange : function(data) {
			var len = data.length;
			for(var i = 0; i < len; i ++) {
				this.dataList.push(data[i]);
			}
		},
		// 选择发生变化.
		selectedChange : function() {
			this.$emit('update:selected', this.selectedItem);
		}
	}
})