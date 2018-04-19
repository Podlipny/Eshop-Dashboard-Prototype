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
        route: null,
        submenu: [
            {
                label: 'All products',
                icon: null,
                route: 'products',
                submenu: null
            },
            {
                label: 'Add new product',
                icon: null,
                route: 'products/add',
                submenu: null
            },
            {
                label: 'Categories',
                icon: null,
                route: 'products/categories',
                submenu: null
            },
            {
                label: 'Vendors',
                icon: null,
                route: 'products/vendors',
                submenu: null
            }
        ],
    },
    {
        label: 'Customers',
        icon: 'assignment_ind',
        route: null,
        submenu: [
            {
                label: 'All customers',
                icon: null,
                route: 'customers',
                submenu: null
            },
            {
                label: 'Add new customer',
                icon: null,
                route: 'customers/add',
                submenu: null
            }
        ],
    },
    {
        label: 'Settings',
        icon: 'settings',
        route: 'settings',
        submenu: null
    }
];
