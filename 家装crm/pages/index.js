import list from '/pages/list/index';

var app = getApp()
Page({
  ...list,

  data: {
    listData: {
      
      data: [{
        
      }
      ]
    }
      
  },

   onItemTap(e){

  // e.currentTarget.dataset.index
    // console.log(app.globalData.pid)
    dd.setStorage({
    key: 'pid',
  data: {
    pid: app.globalData.pid[e.currentTarget.dataset.index].pid
  }
});
   dd.navigateTo({
    url: 'detail/detail'
  })
  },
  onLoad(query) {
    // 页面加载
    console.info(`Page onLoad with query: ${JSON.stringify(query)}`);
  },
  onReady() {
  

  },
  onShow() {
       dd.showLoading({
      content: '登录中...',
      delay: 0,
    });
 var obj=this;
//  var corpId=dd.corpId;
  // dd.alert({content: corpId});
   dd.getAuthCode({
     
    success:(res)=>{
   // dd.alert({content:res.authCode});
        //请求用户信息
  dd.httpRequest({

  url: 'https://crm.microfeel.net/UserLogin/getAuthCode',
  data: {
  authcode: res.authCode
},
  method: 'GET',
  dataType: 'json',
  success: function(res) {
  //  dd.alert({content:JSON.stringify(res)});  res.data.detail.userId
     dd.hideLoading();
        dd.setStorage({
       key: 'uid',
       data: {
       uid:"144229504324161190"
     }
});

dd.showLoading({
      content: '查询...',
      delay: 0,
    });
    
    dd.httpRequest({
      url: 'https://crm.microfeel.net/custom/getCustomerList',
      data:{
        userId:"144229504324161190",
        realcustomer:false
      },
      method: 'GET',
      dataType: 'json',
      success: function(res) {
       console.log(JSON.stringify(res))
       console.log(res.data.projectList)
         dd.hideLoading();
        app.globalData.pid=res.data.projectList;
        var array = [];
        for(let i=0;i<res.data.projectList.length;i++){
          var a=new Object();
          a.thumb ='../image/icon_API.png';
           a.arrow='horizontal';
           if(res.data.projectList[i].address.length>=10){
            a.title=res.data.projectList[i].address.substring(0,10);
           }else{
               a.title=res.data.projectList[i].address;
           }    
          a.extra=res.data.projectList[i].connectdate;
          array.push(a);
        }
          
        obj.setData({
         listData:{
         data:array
      }
      })
      }
      })  
      
      },
  fail: function(res) {

      dd.alert({content:JSON.stringify(res)});
    dd.hideLoading();
  
  },
  complete: function(res) {
  }
});
    },
    fail: (err)=>{



    }
});
  },
  onHide() {
    // 页面隐藏
  },
  onUnload() {
    // 页面被关闭
  },
  onTitleClick() {
  dd.hideLoading();
    // 标题被点击
  },
  onPullDownRefresh() {
    // 页面被下拉
  },
  onReachBottom() {
    // 页面被拉到底部
  },
  onShareAppMessage() {
    // 返回自定义分享信息
    return {
      title: 'My App',
      desc: 'My App description',
      path: 'pages/index/index',
    };
  }
});
