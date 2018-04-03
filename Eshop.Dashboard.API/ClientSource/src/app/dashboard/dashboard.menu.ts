import { MenuItem } from './services/menu.service';

export let dashboardMenuItems: Array<MenuItem> = [
    {
        label: 'Home',
        icon: 'home',
        route: '',
        submenu: null
    },
    {
        label: 'Products',
        icon: 'shopping_cart',
        route: 'products',
        submenu: [
            {
                label: 'All products',
                icon: null,
                route: 'products',
                submenu: null
            },
            {
                label: 'add new product',
                icon: null,
                route: null,
                submenu: null
            }
        ],
    },
    {
        label: 'Customers',
        icon: 'assignment_ind',
        route: 'customers',
        submenu: null
    },
    {
        label: 'Settings',
        icon: 'settings',
        route: 'settings',
        submenu: null
    }
];
