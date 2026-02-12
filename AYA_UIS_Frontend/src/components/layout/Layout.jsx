import React from 'react';
import './Layout.css';

const Layout = ({ children, sidebar = true }) => {
  return (
    <div className="layout">
      <header className="layout__header">
        <div className="layout__header-content">
          <div className="layout__logo">
            <h1 className="layout__title">AYA University</h1>
            <span className="layout__subtitle">Information System</span>
          </div>
          
          <nav className="layout__nav">
            <div className="layout__nav-items">
              <a href="/dashboard" className="layout__nav-link">
                Dashboard
              </a>
              <a href="/departments" className="layout__nav-link">
                Departments
              </a>
              <a href="/courses" className="layout__nav-link">
                Courses
              </a>
              <a href="/fees" className="layout__nav-link">
                Fees
              </a>
              <a href="/schedules" className="layout__nav-link">
                Schedules
              </a>
            </div>
          </nav>

          <div className="layout__user-menu">
            <button className="layout__user-button">
              <span className="layout__user-avatar">User</span>
            </button>
          </div>
        </div>
      </header>

      <div className="layout__container">
        {sidebar && (
          <aside className="layout__sidebar">
            <nav className="layout__sidebar-nav">
              <ul className="layout__sidebar-menu">
                <li className="layout__sidebar-item">
                  <a href="/dashboard" className="layout__sidebar-link">
                    <span className="layout__sidebar-icon">📊</span>
                    Dashboard
                  </a>
                </li>
                <li className="layout__sidebar-item">
                  <a href="/departments" className="layout__sidebar-link">
                    <span className="layout__sidebar-icon">🏛️</span>
                    Departments
                  </a>
                </li>
                <li className="layout__sidebar-item">
                  <a href="/courses" className="layout__sidebar-link">
                    <span className="layout__sidebar-icon">📚</span>
                    Courses
                  </a>
                </li>
                <li className="layout__sidebar-item">
                  <a href="/fees" className="layout__sidebar-link">
                    <span className="layout__sidebar-icon">💰</span>
                    Fees
                  </a>
                </li>
                <li className="layout__sidebar-item">
                  <a href="/schedules" className="layout__sidebar-link">
                    <span className="layout__sidebar-icon">📅</span>
                    Schedules
                  </a>
                </li>
              </ul>
            </nav>
          </aside>
        )}

        <main className={`layout__main ${sidebar ? 'layout__main--with-sidebar' : ''}`}>
          <div className="layout__content">
            {children}
          </div>
        </main>
      </div>

      <footer className="layout__footer">
        <div className="layout__footer-content">
          <p className="layout__footer-text">
            © 2026 AYA University Information System. All rights reserved.
          </p>
        </div>
      </footer>
    </div>
  );
};

export default Layout;