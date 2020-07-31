// eslint-disable-next-line
import { UserLayout, BlankLayout, PageView } from '@/layouts'

/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap = [
  {
    path: '/Home',
    component: UserLayout,
    redirect: '/Home/Login1',
    hidden: true,
    children: [
      {
        path: '/Home/Login',
        name: 'Login',
        component: () => import('@/views/Home/Login')
      }
    ]
  },
  {
    path: '/404',
    component: () => import('@/views/exception/404')
  },
  {
    path: '/Home/Index',
    component: BlankLayout,
    hidden: true,
    children: [{
      path: '/Home/Index',
      name: "Index",
      component: () => import('@/views/Home/Index')
    }]
  }
]
