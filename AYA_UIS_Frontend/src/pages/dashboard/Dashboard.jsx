import React, { useEffect, useState } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import {
  FiBook,
  FiUsers,
  FiGrid,
  FiCalendar,
  FiClipboard,
  FiTrendingUp,
} from 'react-icons/fi';
import { LEVEL_LABELS, GRADE_LABELS } from '../../constants';
import departmentService from '../../services/departmentService';
import courseService from '../../services/courseService';
import registrationService from '../../services/registrationService';
import { userStudyYearService } from '../../services/otherServices';
import './Dashboard.css';

export default function Dashboard() {
  const { user, isAdmin, isStudent } = useAuth();
  const [stats, setStats] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        if (isAdmin) {
          const [depts, courses] = await Promise.all([
            departmentService.getAll().catch(() => null),
            courseService.getAll().catch(() => null),
          ]);
          setStats({
            departments: depts?.data?.length || depts?.length || 0,
            courses: courses?.data?.length || courses?.length || 0,
          });
        } else if (isStudent) {
          const [regs, timeline] = await Promise.all([
            registrationService.getMyCourses().catch(() => []),
            userStudyYearService.getMyTimeline().catch(() => null),
          ]);
          const regData = regs?.data || regs || [];
          setStats({
            registeredCourses: regData.length,
            passedCourses: regData.filter(r => r.isPassed).length,
            timeline: timeline?.data || timeline,
          });
        }
      } catch (e) {
        console.error(e);
      }
      setLoading(false);
    };
    load();
  }, [isAdmin, isStudent]);

  if (loading)
    return (
      <div className="page-container">
        <div className="spinner" />
      </div>
    );

  return (
    <div className="page-container">
      <div className="dashboard-welcome">
        <div>
          <h1>Welcome back, {user?.displayName || 'User'}</h1>
          <p>
            {isAdmin
              ? 'Administrator Dashboard'
              : `${LEVEL_LABELS[user?.level] || ''} — ${user?.departmentName || ''}`}
          </p>
        </div>
        {user?.totalGPA != null && (
          <div className="gpa-badge">
            <FiTrendingUp />
            <span>GPA: {user.totalGPA.toFixed(2)}</span>
          </div>
        )}
      </div>

      {isAdmin && (
        <div className="stats-grid">
          <div className="card stat-card">
            <div className="stat-icon blue">
              <FiGrid />
            </div>
            <div className="stat-info">
              <h3>{stats.departments}</h3>
              <p>Departments</p>
            </div>
          </div>
          <div className="card stat-card">
            <div className="stat-icon green">
              <FiBook />
            </div>
            <div className="stat-info">
              <h3>{stats.courses}</h3>
              <p>Courses</p>
            </div>
          </div>
          <div className="card stat-card">
            <div className="stat-icon orange">
              <FiUsers />
            </div>
            <div className="stat-info">
              <h3>{user?.role}</h3>
              <p>Current Role</p>
            </div>
          </div>
        </div>
      )}

      {isStudent && (
        <>
          <div className="stats-grid">
            <div className="card stat-card">
              <div className="stat-icon blue">
                <FiBook />
              </div>
              <div className="stat-info">
                <h3>{stats.registeredCourses || 0}</h3>
                <p>Registered Courses</p>
              </div>
            </div>
            <div className="card stat-card">
              <div className="stat-icon green">
                <FiClipboard />
              </div>
              <div className="stat-info">
                <h3>{stats.passedCourses || 0}</h3>
                <p>Passed Courses</p>
              </div>
            </div>
            <div className="card stat-card">
              <div className="stat-icon orange">
                <FiCalendar />
              </div>
              <div className="stat-info">
                <h3>
                  {user?.totalCredits || 0} / {user?.allowedCredits || '-'}
                </h3>
                <p>Credits (Earned / Allowed)</p>
              </div>
            </div>
          </div>
          {stats.timeline && (
            <div className="card">
              <h3 style={{ marginBottom: 12 }}>Academic Timeline</h3>
              <p>
                <strong>Current Level:</strong>{' '}
                {stats.timeline.currentLevelName}
              </p>
              <p>
                <strong>Years Completed:</strong>{' '}
                {stats.timeline.totalYearsCompleted}
              </p>
              {stats.timeline.isGraduated && (
                <span className="badge badge-success" style={{ marginTop: 8 }}>
                  Graduated
                </span>
              )}
            </div>
          )}
        </>
      )}
    </div>
  );
}
