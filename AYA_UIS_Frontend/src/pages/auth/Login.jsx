import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { ROUTES, STATUS } from '../../constants';
import logo from '../../assets/images/logo.svg';
import './Login.css';

export default function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const { login, status, error, clearError } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async e => {
    e.preventDefault();
    clearError();
    try {
      await login(email, password);
      navigate(ROUTES.DASHBOARD);
    } catch (_) {}
  };

  return (
    <div className="login-page">
      <div className="login-left">
        <div className="login-branding">
          <img src={logo} alt="AYA Academy" className="login-logo" />
          <h1>AYA Academy</h1>
          <p>University Information System</p>
        </div>
        <div className="login-features">
          <div className="feature-item">
            <span className="feature-dot" />
            <span>Course Registration & Management</span>
          </div>
          <div className="feature-item">
            <span className="feature-dot" />
            <span>Academic Progress Tracking</span>
          </div>
          <div className="feature-item">
            <span className="feature-dot" />
            <span>Department & Schedule Management</span>
          </div>
        </div>
      </div>
      <div className="login-right">
        <div className="login-card">
          <h2>Welcome Back</h2>
          <p className="login-subtitle">Sign in to your account</p>
          {error && <div className="login-error">{error}</div>}
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label>Email Address</label>
              <input
                type="email"
                className="form-control"
                value={email}
                onChange={e => setEmail(e.target.value)}
                placeholder="your@email.com"
                required
              />
            </div>
            <div className="form-group">
              <label>Password</label>
              <input
                type="password"
                className="form-control"
                value={password}
                onChange={e => setPassword(e.target.value)}
                placeholder="Enter your password"
                required
              />
            </div>
            <button
              type="submit"
              className="btn btn-primary login-btn"
              disabled={status === STATUS.LOADING}
            >
              {status === STATUS.LOADING ? 'Signing in...' : 'Sign In'}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
